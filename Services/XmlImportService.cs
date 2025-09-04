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
                    Name = (string)t.Element(ns + "Name"),
                    Wbs = (string)t.Element(ns + "WBS"),
                    Start = (DateTime?)t.Element(ns + "Start"),
                    Finish = (DateTime?)t.Element(ns + "Finish"),
                    IsSummary = ((int?)t.Element(ns + "Summary") ?? 0) == 1
                })
                .Where(t => t.Uid != 0) // Filtrar la tarea resumen del proyecto (UID 0)
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
                    EsResumen = taskData.IsSummary
                };
                nuevasTareas.Add(nuevaTarea);
            }

            await context.Tareas.AddRangeAsync(nuevasTareas);
            await context.SaveChangesAsync();

            // PASO 3: CONECTAR JERARQUÍA
            var tareasDelProyecto = await context.Tareas
                .Where(t => t.ProyectoId == proyectoId)
                .ToListAsync();

            var wbsMap = tareasDelProyecto.ToDictionary(t => t.WBS, t => t);

            foreach (var tarea in tareasDelProyecto)
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

            // PASO 4: CONECTAR DEPENDENCIAS
            var uidMap = tareasDelProyecto.ToDictionary(t => t.UidMsProject, t => t.TareaId);
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
    }
}
