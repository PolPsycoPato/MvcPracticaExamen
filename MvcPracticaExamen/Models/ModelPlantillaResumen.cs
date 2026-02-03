using System.Collections.Generic;

namespace MvcPracticaExamen.Models
{
    public class ModelPlantillaResumen
    {
        public List<Plantilla> Plantilla { get; set; }

        public int SumaSalarial { get; set; }
        public int MaximoSalario { get; set; }
        public double MediaSalarial { get; set; }
        public string Apellido { get; set; }
        public string Funcion { get; set; }
        public int Salario { get; set; }
    }

}