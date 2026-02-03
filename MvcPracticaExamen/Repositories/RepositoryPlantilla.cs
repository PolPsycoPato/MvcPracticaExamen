using Microsoft.Data.SqlClient;
using System.Data;
using MvcPracticaExamen.Models;

namespace MvcPracticaExamen.Repositories
{
    public class RepositoryPlantilla
    {
        private string connectionString;

        public RepositoryPlantilla(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("SqlHospital");
        }

        // --- LECTURAS ---

        public List<string> GetFunciones()
        {
            List<string> funciones = new List<string>();
            using (SqlConnection cn = new SqlConnection(this.connectionString))
            {
                SqlCommand com = new SqlCommand("SP_GET_FUNCIONES", cn);
                com.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    funciones.Add(reader["FUNCION"].ToString());
                }
            }
            return funciones;
        }

        public ModelPlantillaResumen GetResumenPorFuncion(string funcion)
        {
            string spName = "SP_GET_PLANTILLA_FILTRO";
            SqlParameter param = new SqlParameter("@FUNCION", funcion);

            List<Plantilla> empleados = ExecuteQueryPlantilla(spName, param);

            ModelPlantillaResumen model = new ModelPlantillaResumen();
            model.Plantilla = empleados;

            if (empleados != null && empleados.Count > 0)
            {
                model.MaximoSalario = empleados.Max(x => x.Salario);
                model.SumaSalarial = empleados.Sum(x => x.Salario);
                model.MediaSalarial = (double)empleados.Average(x => x.Salario);
            }
            return model;
        }

        public Plantilla GetEmpleadoDetalle(int idEmpleado)
        {
            string spName = "SP_GET_EMPLEADO_DETALLE";
            SqlParameter param = new SqlParameter("@EMPLEADO_NO", idEmpleado);
            var lista = ExecuteQueryPlantilla(spName, param);

            // Si la lista está vacía, devolverá null y activará tu redirección
            return lista.FirstOrDefault();
        }

        // --- ACCIONES (UPSERT y DELETE) ---

        public void UpsertPlantilla(MvcPracticaExamen.Models.Plantilla p)
        {
            using (SqlConnection cn = new SqlConnection(this.connectionString))
            {
                SqlCommand com = new SqlCommand("SP_PLANTILLA_UPSERT", cn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@HOSPITAL_COD", p.HospitalCod);
                com.Parameters.AddWithValue("@SALA_COD", p.SalaCod);
                com.Parameters.AddWithValue("@EMPLEADO_NO", p.EmpleadoNo);
                com.Parameters.AddWithValue("@APELLIDO", p.Apellido);
                com.Parameters.AddWithValue("@FUNCION", p.Funcion);
                // Control de nulos seguro
                if (p.T == null) com.Parameters.AddWithValue("@T", DBNull.Value);
                else com.Parameters.AddWithValue("@T", p.T);

                com.Parameters.AddWithValue("@SALARIO", p.Salario);

                cn.Open();
                com.ExecuteNonQuery();
            }
        }

        public void DeletePlantilla(int id)
        {
            using (SqlConnection cn = new SqlConnection(this.connectionString))
            {
                SqlCommand com = new SqlCommand("SP_DELETE_PLANTILLA", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EMPLEADO_NO", id);
                cn.Open();
                com.ExecuteNonQuery();
            }
        }

        // --- MÉTODO CLAVE: EL MAPEO ---
        // Aquí es donde probablemente estaba fallando la lectura del ID
        private List<Plantilla> ExecuteQueryPlantilla(string spName, SqlParameter param)
        {
            List<Plantilla> lista = new List<Plantilla>();
            using (SqlConnection cn = new SqlConnection(this.connectionString))
            {
                SqlCommand com = new SqlCommand(spName, cn);
                com.CommandType = CommandType.StoredProcedure;

                if (param != null) com.Parameters.Add(param);

                cn.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    var emp = new Plantilla();

                    // Mapeo robusto: convierte a String primero para evitar errores de tipo
                    emp.EmpleadoNo = int.Parse(reader["EMPLEADO_NO"].ToString());
                    emp.Apellido = reader["APELLIDO"].ToString();
                    emp.Funcion = reader["FUNCION"].ToString();
                    emp.Salario = int.Parse(reader["SALARIO"].ToString());

                    // Manejo de posibles nulos en la BDD
                    if (reader["HOSPITAL_COD"] != DBNull.Value)
                        emp.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());

                    if (reader["SALA_COD"] != DBNull.Value)
                        emp.SalaCod = int.Parse(reader["SALA_COD"].ToString());

                    if (reader["T"] != DBNull.Value)
                        emp.T = reader["T"].ToString();

                    lista.Add(emp);
                }
            }
            return lista;
        }
    }
}