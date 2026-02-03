using System.Collections.Generic;

namespace MvcPracticaExamen.Models
{
    public class ModelPlantillaResumen
    {
        // CAMBIO CLAVE: Usamos 'Plantilla' (la clase completa) y llamamos a la lista 'Plantilla'
        public List<Plantilla> Plantilla { get; set; }

        // Usamos tus nombres largos que vi en tu código
        public int SumaSalarial { get; set; }
        public int MaximoSalario { get; set; }
        public double MediaSalarial { get; set; }
        public string Apellido { get; set; }
        public string Funcion { get; set; }
        public int Salario { get; set; }
    }

}