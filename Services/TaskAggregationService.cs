using System;
using System.Collections.Generic;
using System.Linq;
using WorkFlow_SIG10._1.Models;

namespace WorkFlow_SIG10._1.Services
{
    public class TaskAggregationService
    {
        public void CalculateSummaryTasks(List<Tarea> allTasks)
        {
            // Build the in-memory tree structure if not already built
            // This is crucial because the Tarea objects from the DB might not have their Subtareas populated
            var tasksById = allTasks.ToDictionary(t => t.TareaId);
            foreach (var task in allTasks)
            {
                task.Subtareas.Clear(); // Clear existing to avoid duplicates on re-calculation
                if (task.TareaPadreId.HasValue && tasksById.TryGetValue(task.TareaPadreId.Value, out var parentTask))
                {
                    parentTask.Subtareas.Add(task);
                }
            }

            // Identify top-level tasks (those without a parent)
            var topLevelTasks = allTasks.Where(t => !t.TareaPadreId.HasValue).ToList();

            // Calculate from top-level tasks downwards
            foreach (var task in topLevelTasks)
            {
                CalculateTaskValuesRecursive(task);
            }
        }

        private void CalculateTaskValuesRecursive(Tarea tarea)
        {
            if (tarea.Subtareas != null && tarea.Subtareas.Any())
            {
                // Ensure all subtasks are calculated first
                foreach (var subtarea in tarea.Subtareas)
                {
                    CalculateTaskValuesRecursive(subtarea);
                }

                // Only calculate for summary tasks based on their real progress
                // For projected values, the XML import already handles it.
                // This service focuses on real progress aggregation.

                // Calculate FechaInicioReal (minimum of subtasks' FechaInicioReal)
                tarea.FechaInicioReal = tarea.Subtareas.Min(s => s.FechaInicioReal);

                // Calculate FechaFinReal (maximum of subtasks' FechaFinReal)
                tarea.FechaFinReal = tarea.Subtareas.Max(s => s.FechaFinReal);

                // Calculate DuracionReal (based on FechaInicioReal and FechaFinReal)
                if (tarea.FechaInicioReal.HasValue && tarea.FechaFinReal.HasValue)
                {
                    tarea.DuracionReal = (int)((tarea.FechaFinReal.Value - tarea.FechaInicioReal.Value).TotalDays + 1);
                }
                else
                {
                    tarea.DuracionReal = null; // Or 0, depending on desired behavior for incomplete dates
                }

                // Calculate PorcentajeCompletadoReal (weighted average by real duration)
                var totalDuracionRealSubtareas = tarea.Subtareas.Sum(s => s.DuracionReal ?? 0); // Use DuracionReal
                if (totalDuracionRealSubtareas > 0)
                {
                    tarea.PorcentajeCompletadoReal = (int?)(tarea.Subtareas.Sum(s => (s.PorcentajeCompletadoReal ?? 0) * (s.DuracionReal ?? 0)) / totalDuracionRealSubtareas);
                }
                else
                {
                    tarea.PorcentajeCompletadoReal = 0;
                }

                // Determine EstadoAccion
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
                else if (tarea.Subtareas.Any(s => s.EstadoAccion == EstadoAccionTarea.PorEjecutar))
                {
                    tarea.EstadoAccion = EstadoAccionTarea.PorEjecutar;
                }
                else
                {
                    tarea.EstadoAccion = EstadoAccionTarea.Desconocido; // Default or handle other cases
                }
            }
        }
    }
}
