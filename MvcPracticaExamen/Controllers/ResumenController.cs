using Microsoft.AspNetCore.Mvc;
using MvcPracticaExamen.Models;    
using MvcPracticaExamen.Repositories;

namespace MvcPracticaExamen.Controllers
{
    public class ResumenController : Controller
    {
        private RepositoryPlantilla repo;

        public ResumenController(RepositoryPlantilla repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string funcion)
        {
            ViewBag.Funciones = repo.GetFunciones();
            ViewData["Funcion"] = funcion;

            if (string.IsNullOrEmpty(funcion))
            {
              
                return View(new ModelPlantillaResumen());
            }

      
            ModelPlantillaResumen model = repo.GetResumenPorFuncion(funcion);
            return View(model);
        }

 

        [HttpPost]
        public IActionResult Formulario(Plantilla p)
        {
            repo.UpsertPlantilla(p);
            return RedirectToAction("Index", new { funcion = p.Funcion });
        }

        public IActionResult Delete(int id, string funcionActual)
        {
            repo.DeletePlantilla(id);
            return RedirectToAction("Index", new { funcion = funcionActual });
        }
        public IActionResult Formulario(int? id)
        {
            if (id != null)
            {
                Plantilla p = repo.GetEmpleadoDetalle(id.Value);

                if (p == null)
                {
                    return RedirectToAction("Index");
                }

                return View(p);
            }
            return View(new Plantilla());
        }
    }
}