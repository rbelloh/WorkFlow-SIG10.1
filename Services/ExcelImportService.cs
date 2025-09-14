using WorkFlow_SIG10._1.Data;
using Microsoft.EntityFrameworkCore;

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

            // Aquí iría la lógica para leer el archivo Excel y actualizar la base de datos.
            // Por ahora, es solo un placeholder.

            await Task.Delay(1000); // Simula trabajo asíncrono

            _logger.LogInformation("La funcionalidad de importación de presupuesto desde Excel aún no está implementada.");

            // Lanza una excepción para indicar que la funcionalidad no está implementada
            throw new NotImplementedException("La funcionalidad de importación de presupuesto desde Excel aún no está implementada.");
        }
    }
}
