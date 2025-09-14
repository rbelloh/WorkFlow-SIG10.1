using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkFlow_SIG10._1.Data;
using WorkFlow_SIG10._1.Models;

namespace WorkFlow_SIG10._1.Services
{
    public class XmlImportService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public XmlImportService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task ImportarTareasDesdeXmlAsync(Stream xmlStream, int proyectoId)
        {
            using var context = _contextFactory.CreateDbContext();

            var xDoc = await XDocument.LoadAsync(xmlStream, LoadOptions.None, CancellationToken.None);
            XNamespace ns = "http://schemas.microsoft.com/project";

            // PASO 1: LEER TAREAS DEL XML
            var tasks = xDoc.Descendants(ns + "Task")
                .Select(t => new
                {
                    Uid = (int?)t.Element(ns + "UID") ?? 0,
                    Name = t.Element(ns + "Name")?.Value ?? string.Empty,
                    Wbs = t.Element(ns + "WBS")?.Value ?? string.Empty,
                    Start = (DateTime?)t.Element(ns + "Start"),
                    Finish = (DateTime?)t.Element(ns + "Finish"),
                    IsSummary = ((int?)t.Element(ns + "Summary") ?? 0) == 1
                })
                .ToList();

            // Limpiar tareas y dependencias existentes para este proyecto para evitar duplicados
            var tareasAntiguas = await context.Tareas.Where(t => t.ProyectoId == proyectoId).ToListAsync();
            if (tareasAntiguas.Any())
            {
                var dependenciasAntiguas = await context.DependenciaTareas
                    .Where(d => tareasAntiguas.Select(t => t.TareaId).Contains(d.TareaSucesoraId) || tareasAntiguas.Select(t => t.TareaId).Contains(d.TareaPredecesoraId))
                    .ToListAsync();
                context.DependenciaTareas.RemoveRange(dependenciasAntiguas);
                context.Tareas.RemoveRange(tareasAntiguas);
                await context.SaveChangesAsync();
            }

            // PASO 2: GUARDAR TAREAS (SIN RELACIONES)
            var nuevasTareas = new List<Tarea>();
            foreach (var taskData in tasks)
            {
                var nuevaTarea = new Tarea
                {
                    ProyectoId = proyectoId,
                    UidMsProject = taskData.Uid,
                    Nombre = taskData.Name,
                    WBS = taskData.Wbs,
                    FechaInicio = taskData.Start ?? DateTime.MinValue,
                    FechaFin = taskData.Finish ?? DateTime.MinValue,
                    EsResumen = taskData.IsSummary,
                    EstadoAccion = EstadoAccionTarea.PorEjecutar, // Valor por defecto para nuevas tareas
                    Notas = string.Empty // Añadir valor por defecto para evitar error de nulo
                };
                nuevasTareas.Add(nuevaTarea);
            }

            await context.Tareas.AddRangeAsync(nuevasTareas);
            await context.SaveChangesAsync();

            // PASO 3: CONECTAR JERARQUÍA
            var todasLasTareas = await context.Tareas
                .Where(t => t.ProyectoId == proyectoId)
                .ToListAsync();

            var wbsMap = todasLasTareas.ToDictionary(t => t.WBS, t => t);

            foreach (var tarea in todasLasTareas)
            {
                if (string.IsNullOrEmpty(tarea.WBS) || !tarea.WBS.Contains('.'))
                {
                    continue; // No buscar padre para tareas de nivel 1
                }

                var wbsPadre = tarea.WBS.Substring(0, tarea.WBS.LastIndexOf('.'));

                if (wbsMap.TryGetValue(wbsPadre, out var tareaPadre))
                {
                    tarea.TareaPadreId = tareaPadre.TareaId;
                }
            }
            
            await context.SaveChangesAsync();

            // PASO 4: CALCULAR VALORES DE TAREAS DE RESUMEN
            // Primero, construir el árbol en memoria para facilitar el cálculo recursivo
            var tareasPorId = todasLasTareas.ToDictionary(t => t.TareaId);
            foreach (var tarea in todasLasTareas)
            {
                if (tarea.TareaPadreId.HasValue && tareasPorId.TryGetValue(tarea.TareaPadreId.Value, out var padre))
                {
                    padre.Subtareas.Add(tarea); // Asegurarse de que las subtareas estén cargadas
                }
            }

            // Calcular desde las tareas de nivel superior hacia abajo
            foreach (var tareaRaiz in todasLasTareas.Where(t => !t.TareaPadreId.HasValue))
            {
                CalcularValoresTareasResumen(tareaRaiz);
            }

            await context.SaveChangesAsync(); // Guardar los valores calculados

            // PASO 5: CONECTAR DEPENDENCIAS
            var uidMap = todasLasTareas.ToDictionary(t => t.UidMsProject, t => t.TareaId);
            var dependencias = new List<DependenciaTarea>();

            var predecessorLinks = xDoc.Descendants(ns + "PredecessorLink");

            foreach (var link in predecessorLinks)
            {
                var predecessorUid = (int?)link.Element(ns + "PredecessorUID");
                var successorUid = (int?)link.Element(ns + "SuccessorUID");

                if (predecessorUid.HasValue && successorUid.HasValue &&
                    uidMap.TryGetValue(predecessorUid.Value, out var predecessorId) &&
                    uidMap.TryGetValue(successorUid.Value, out var successorId))
                {
                    dependencias.Add(new DependenciaTarea
                    {
                        TareaPredecesoraId = predecessorId,
                        TareaSucesoraId = successorId
                    });
                }
            }

            if (dependencias.Any())
            {
                await context.DependenciaTareas.AddRangeAsync(dependencias);
                await context.SaveChangesAsync();
            }
        }

        private void CalcularValoresTareasResumen(Tarea tarea)
        {
            if (tarea.Subtareas != null && tarea.Subtareas.Any())
            {
                // Asegurarse de que las subtareas también estén calculadas
                foreach (var subtarea in tarea.Subtareas)
                {
                    CalcularValoresTareasResumen(subtarea);
                }

                // Calcular FechaInicio de la tarea resumen (mínimo de las subtareas)
                tarea.FechaInicio = tarea.Subtareas.Min(s => s.FechaInicio);
                // Calcular FechaFin de la tarea resumen (máximo de las subtareas)
                tarea.FechaFin = tarea.Subtareas.Max(s => s.FechaFin);

                // Calcular Duracion (en días, basado en FechaInicio y FechaFin)
                tarea.DuracionReal = (int)(tarea.FechaFin - tarea.FechaInicio).TotalDays + 1;

                // Calcular PorcentajeCompletadoReal (promedio ponderado por duración)
                var totalDuracionSubtareas = tarea.Subtareas.Sum(s => (s.FechaFin - s.FechaInicio).TotalDays + 1);
                if (totalDuracionSubtareas > 0)
                {
                    tarea.PorcentajeCompletadoReal = (int?)(tarea.Subtareas.Sum(s => (s.PorcentajeCompletadoReal ?? 0) * ((s.FechaFin - s.FechaInicio).TotalDays + 1)) / totalDuracionSubtareas);
                }
                else
                {
                    tarea.PorcentajeCompletadoReal = 0;
                }

                // Determinar EstadoAccion y Notas para tareas resumen (simplificado)
                if (tarea.PorcentajeCompletadoReal == 100)
                {
                    tarea.EstadoAccion = EstadoAccionTarea.Finalizada;
                }
                else if (tarea.Subtareas.Any(s => s.EstadoAccion == EstadoAccionTarea.EnEjecucion))
                {
                    tarea.EstadoAccion = EstadoAccionTarea.EnEjecucion;
                }
                else if (tarea.Subtareas.Any(s => s.EstadoAccion == EstadoAccionTarea.Retrasada))
                {
                    tarea.EstadoAccion = EstadoAccionTarea.Retrasada;
                }
                else
                {
                    tarea.EstadoAccion = EstadoAccionTarea.PorEjecutar;
                }
                // Las notas de resumen pueden ser un agregado o simplemente vacías
                tarea.Notas = ""; 
            }
        }

        public async Task UpdateProjectFromXmlAsync(int projectId, Stream xmlStream)
        {
            using var context = _contextFactory.CreateDbContext();

            var xDoc = await XDocument.LoadAsync(xmlStream, LoadOptions.None, CancellationToken.None);
            XNamespace ns = "http://schemas.microsoft.com/project";

            // Extraer y actualizar las fechas de inicio y fin del proyecto desde el XML
            var startDateStr = xDoc.Root?.Element(ns + "StartDate")?.Value;
            var finishDateStr = xDoc.Root?.Element(ns + "FinishDate")?.Value;

            if (DateTime.TryParse(startDateStr, out var startDate) && DateTime.TryParse(finishDateStr, out var finishDate))
            {
                var projectToUpdate = await context.Proyectos.FindAsync(projectId);
                if (projectToUpdate != null)
                {
                    projectToUpdate.FechaInicioProyecto = startDate;
                    projectToUpdate.FechaTerminoProyecto = finishDate;
                }
            }

            // 1. Leer tareas del XML y preparar un mapa de UIDs
            var xmlTasksData = xDoc.Descendants(ns + "Task")
                .Select(t => new
                {
                    Uid = (int?)t.Element(ns + "UID") ?? 0,
                    Name = t.Element(ns + "Name")?.Value ?? string.Empty,
                    Wbs = t.Element(ns + "WBS")?.Value ?? string.Empty,
                    Start = (DateTime?)t.Element(ns + "Start"),
                    Finish = (DateTime?)t.Element(ns + "Finish"),
                    IsSummary = ((int?)t.Element(ns + "Summary") ?? 0) == 1,
                    ParentUid = (int?)t.Element(ns + "ParentUID") // Assuming ParentUID exists in XML for hierarchy
                })
                .ToList();

            var xmlTasksByUid = xmlTasksData.ToDictionary(t => t.Uid);

            // 2. Obtener tareas existentes del proyecto desde la DB
            var existingTasks = await context.Tareas
                .Where(t => t.ProyectoId == projectId)
                .Include(t => t.Predecesoras)
                .Include(t => t.Sucesoras)
                .ToListAsync();

            var existingTasksByUid = existingTasks.ToDictionary(t => t.UidMsProject);

            var tasksToUpdate = new List<Tarea>();
            var tasksToAdd = new List<Tarea>();
            var processedExistingUids = new HashSet<int>();

            // 3. Procesar tareas del XML: Actualizar existentes o añadir nuevas
            foreach (var xmlTask in xmlTasksData)
            {
                if (existingTasksByUid.TryGetValue(xmlTask.Uid, out var existingTask))
                {
                    // Tarea existente: Actualizar solo campos planificados
                    existingTask.Nombre = xmlTask.Name;
                    existingTask.WBS = xmlTask.Wbs;
                    existingTask.FechaInicio = xmlTask.Start ?? DateTime.MinValue;
                    existingTask.FechaFin = xmlTask.Finish ?? DateTime.MinValue;
                    existingTask.EsResumen = xmlTask.IsSummary;
                    // No actualizar campos reales (FechaInicioReal, FechaFinReal, PorcentajeCompletadoReal, Notas)

                    tasksToUpdate.Add(existingTask);
                    processedExistingUids.Add(xmlTask.Uid);
                }
                else
                {
                    // Nueva tarea: Añadir
                    var newTask = new Tarea
                    {
                        ProyectoId = projectId,
                        UidMsProject = xmlTask.Uid,
                        Nombre = xmlTask.Name,
                        WBS = xmlTask.Wbs,
                        FechaInicio = xmlTask.Start ?? DateTime.MinValue,
                        FechaFin = xmlTask.Finish ?? DateTime.MinValue,
                        EsResumen = xmlTask.IsSummary,
                        EstadoAccion = EstadoAccionTarea.PorEjecutar, // Valor por defecto
                        Notas = string.Empty, // Default value
                        Unidad = string.Empty, // Default value for Unidad
                        // Inicializar campos reales a null/default
                        FechaInicioReal = null,
                        FechaFinReal = null,
                        PorcentajeCompletadoReal = 0,
                        DuracionReal = null
                    };
                    tasksToAdd.Add(newTask);
                }
            }

            // 4. Eliminar tareas que ya no están en el XML
            var tasksToDelete = existingTasks
                .Where(t => !processedExistingUids.Contains(t.UidMsProject))
                .ToList();

            if (tasksToDelete.Any())
            {
                // Eliminar dependencias asociadas a las tareas a eliminar
                var dependenciesToDelete = await context.DependenciaTareas
                    .Where(d => tasksToDelete.Select(t => t.TareaId).Contains(d.TareaSucesoraId) || tasksToDelete.Select(t => t.TareaId).Contains(d.TareaPredecesoraId))
                    .ToListAsync();
                context.DependenciaTareas.RemoveRange(dependenciesToDelete);

                context.Tareas.RemoveRange(tasksToDelete);
            }

            // Add new tasks
            if (tasksToAdd.Any())
            {
                await context.Tareas.AddRangeAsync(tasksToAdd);
            }

            await context.SaveChangesAsync(); // Save changes so new tasks get IDs and deleted tasks are removed

            // 5. Reconstruir jerarquía (TareaPadreId) y dependencias
            // Necesitamos recargar todas las tareas (incluyendo las nuevas con IDs) para reconstruir relaciones
            var allTasksInDb = await context.Tareas
                .Where(t => t.ProyectoId == projectId)
                .ToListAsync();

            var currentUidMap = allTasksInDb.ToDictionary(t => t.UidMsProject, t => t.TareaId);
            var currentWbsMap = allTasksInDb.ToDictionary(t => t.WBS, t => t);

            // Update ParentUIDs based on WBS hierarchy (assuming WBS reflects hierarchy)
            foreach (var task in allTasksInDb)
            {
                if (string.IsNullOrEmpty(task.WBS) || !task.WBS.Contains('.'))
                {
                    task.TareaPadreId = null; // Top-level task
                    continue;
                }

                var wbsPadre = task.WBS.Substring(0, task.WBS.LastIndexOf('.'));
                if (currentWbsMap.TryGetValue(wbsPadre, out var parentTask))
                {
                    task.TareaPadreId = parentTask.TareaId;
                }
                else
                {
                    task.TareaPadreId = null; // Parent not found, make it top-level
                }
            }

            // Clear existing dependencies for the project before adding new ones
            var existingProjectDependencies = await context.DependenciaTareas
                .Where(d => allTasksInDb.Select(t => t.TareaId).Contains(d.TareaSucesoraId) || allTasksInDb.Select(t => t.TareaId).Contains(d.TareaPredecesoraId))
                .ToListAsync();
            context.DependenciaTareas.RemoveRange(existingProjectDependencies);

            // Add new dependencies from XML
            var newDependencies = new List<DependenciaTarea>();
            var predecessorLinks = xDoc.Descendants(ns + "PredecessorLink");

            foreach (var link in predecessorLinks)
            {
                var predecessorUid = (int?)link.Element(ns + "PredecessorUID");
                var successorUid = (int?)link.Element(ns + "SuccessorUID");

                if (predecessorUid.HasValue && successorUid.HasValue &&
                    currentUidMap.TryGetValue(predecessorUid.Value, out var predecessorId) &&
                    currentUidMap.TryGetValue(successorUid.Value, out var successorId))
                {
                    newDependencies.Add(new DependenciaTarea
                    {
                        TareaPredecesoraId = predecessorId,
                        TareaSucesoraId = successorId
                    });
                }
            }

            if (newDependencies.Any())
            {
                await context.DependenciaTareas.AddRangeAsync(newDependencies);
            }

            await context.SaveChangesAsync(); // Save hierarchy and dependencies

            // 6. Recalcular valores de tareas resumen
            // Rebuild the in-memory hierarchy for summary task calculation
            var tasksForAggregation = await context.Tareas
                .Where(t => t.ProyectoId == projectId)
                .Include(t => t.Subtareas) // Ensure Subtareas are loaded for calculation
                .ToListAsync();

            // Build hierarchy in memory (Tarea.Subtareas is not automatically populated by EF Core unless explicitly loaded)
            var tasksById = tasksForAggregation.ToDictionary(t => t.TareaId);
            foreach (var task in tasksForAggregation)
            {
                if (task.TareaPadreId.HasValue && tasksById.TryGetValue(task.TareaPadreId.Value, out var parentTask))
                {
                    parentTask.Subtareas.Add(task);
                }
            }

            // Calculate from top-level tasks down
            foreach (var rootTask in tasksForAggregation.Where(t => !t.TareaPadreId.HasValue))
            {
                CalcularValoresTareasResumen(rootTask);
            }

            await context.SaveChangesAsync(); // Save calculated summary values
        }
    }
}
