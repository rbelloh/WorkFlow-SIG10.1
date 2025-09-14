using WorkFlow_SIG10._1.Data;
using WorkFlow_SIG10._1.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel; // Usamos la interfaz genérica
using NPOI.XSSF.UserModel; // Aún necesario para WorkbookFactory

namespace WorkFlow_SIG10._1.Services
{
    public class ExcelImportService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly ILogger<ExcelImportService> _logger;

        public ExcelImportService(IDbContextFactory<ApplicationDbContext> dbFactory, ILogger<ExcelImportService> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task UpdateBudgetFromExcelAsync(int projectId, Stream fileStream)
        {
            _logger.LogInformation("Iniciando la actualización del presupuesto desde Excel para el proyecto {ProjectId}.", projectId);
            using var context = _dbFactory.CreateDbContext();
            
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var existingItems = context.Tareas.Where(t => t.ProyectoId == projectId);
                context.Tareas.RemoveRange(existingItems);
                await context.SaveChangesAsync();
                _logger.LogInformation("Items de presupuesto existentes para el proyecto {ProjectId} han sido eliminados.", projectId);

                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // --- CAMBIO CLAVE #1: Usamos WorkbookFactory para compatibilidad con .xls y .xlsx ---
                IWorkbook workbook = WorkbookFactory.Create(memoryStream);
                ISheet sheet = workbook.GetSheetAt(0);

                if (sheet == null)
                {
                    throw new Exception("El archivo Excel no contiene ninguna hoja.");
                }

                var newItems = new List<Tarea>();
                // --- CAMBIO CLAVE #2: Lógica de lectura inteligente ---
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    // Si la fila no existe, o las celdas clave están vacías/no son válidas, la saltamos.
                    if (row == null || 
                        row.GetCell(0) == null || string.IsNullOrWhiteSpace(row.GetCell(0).ToString()) ||
                        row.GetCell(3) == null || row.GetCell(3).CellType != CellType.Numeric)
                    {
                        continue; // Ignora títulos, cabeceras, subtotales y filas vacías
                    }

                    var tarea = new Tarea
                    {
                        ProyectoId = projectId,
                        WBS = row.GetCell(0)?.ToString()?.Trim() ?? string.Empty,
                        Nombre = row.GetCell(1)?.ToString()?.Trim() ?? string.Empty,
                        Unidad = row.GetCell(2)?.ToString()?.Trim() ?? string.Empty,
                        CantidadContrato = GetDecimalFromCell(row.GetCell(3)),
                        PrecioUnitario = GetDecimalFromCell(row.GetCell(4)),
                        ImporteContrato = GetDecimalFromCell(row.GetCell(5)),
                        UidMsProject = i, // Usamos el número de fila como UID temporal
                        EsResumen = false, // Asumimos que no hay tareas de resumen en un presupuesto plano
                        FechaInicio = DateTime.Now,
                        FechaFin = DateTime.Now,
                    };
                    
                    newItems.Add(tarea);
                }

                if (newItems.Count == 0)
                {
                    throw new Exception("No se encontraron filas con datos válidos en el archivo Excel. Verifique que las columnas 'ITEM' y 'CANTIDAD' contengan datos.");
                }

                await context.Tareas.AddRangeAsync(newItems);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation("Se han importado {Count} nuevos ítems de presupuesto para el proyecto {ProjectId}.", newItems.Count, projectId);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Ocurrió un error al actualizar el presupuesto desde Excel para el proyecto {ProjectId}.", projectId);
                // No envolvemos la excepción para obtener el mensaje de error real en la UI
                throw; 
            }
        }
        
        private decimal GetDecimalFromCell(ICell cell)
        {
            if (cell == null) return 0;

            if (cell.CellType == CellType.Numeric)
            {
                return (decimal)cell.NumericCellValue;
            }
            // A veces los totales vienen de fórmulas, NPOI puede leer el resultado
            if (cell.CellType == CellType.Formula)
            {
                try {
                    return (decimal)cell.NumericCellValue;
                } catch {
                    return 0;
                }
            }
            if (cell.CellType == CellType.String)
            {
                if (decimal.TryParse(cell.StringCellValue, out var decValue))
                {
                    return decValue;
                }
            }
            return 0;
        }
    }
}
