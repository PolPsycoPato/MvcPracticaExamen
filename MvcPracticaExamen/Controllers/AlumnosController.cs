using Microsoft.AspNetCore.Mvc;
using MvcPracticaExamen.Repositories;

namespace MvcPracticaExamen.Controllers
{
    public class AlumnosController : Controller
    {
        public RepositoryAlumno repo;
         
        public AlumnosController()
        {
            this.repo = new RepositoryAlumno();
        }

        public IActionResult Index()
        {

            var alumnos = this.repo.GetAlumnos();
            return View(alumnos);
        }
        public IActionResult Detalles(int id)
        {

            var alumno = this.repo.GetDetails(id);
            return View(alumno);
        }

      

    }
}
