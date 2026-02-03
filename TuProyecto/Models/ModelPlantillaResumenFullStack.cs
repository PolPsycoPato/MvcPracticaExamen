namespace TuProyecto.Models
{
    public class ModelPlantillaResumenFullStack
    {
        // Add properties as needed, for example:
        public List<Empleado> Empleados { get; set; }
        public string FuncionSeleccionada { get; set; }
        public decimal MaximoSalario { get; set; }
        public decimal MediaSalarial { get; set; }
        public decimal SumaSalarial { get; set; }
    }

    // Example Empleado class (adjust as needed)
    public class Empleado
    {
        public int EmpleadoNo { get; set; }
        public string Apellido { get; set; }
        public string Funcion { get; set; }
        public decimal Salario { get; set; }
        public string T { get; set; }
    }
}