using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using MvcPracticaExamen.Models;
using System.Data;

namespace MvcPracticaExamen.Repositories
{
    public class RepositoryAlumno
    {
        DataTable tablaAlumnos;
        public RepositoryAlumno()
        {
            string connectionString = "Data Source=.;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=;Encrypt=True;Trust Server Certificate=True";

            string sql = "SELECT * FROM vw_inscripciones_completa";
            SqlDataAdapter ad = new SqlDataAdapter(sql,connectionString);
            this.tablaAlumnos = new DataTable();
            ad.Fill(this.tablaAlumnos);

        }


        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.tablaAlumnos.AsEnumerable() select datos;
            if (consulta == null)
            {
                return null;
            }
            else
            {
                List<Alumno> al = new List<Alumno>();
                foreach (var fila in consulta)
                {
                    al.Add(
                        new Alumno
                        {
                            IDCURSO = fila.Field<int>("IDCURSO"),
                            NombreCurso = fila.Field<string?>("NombreCurso"),
                            FECHAINICIO = fila.Field<DateTime?>("FECHAINICIO"),
                            FECHAFIN = fila.Field<DateTime?>("FECHAFIN"),
                            ACTIVO = fila.Field<bool?>("ACTIVO"),
                            IDUSUARIO = fila.Field<int>("IDUSUARIO"),
                            NombreUsuario = fila.Field<string?>("NombreUsuario"),
                            APELLIDOS = fila.Field<string?>("APELLIDOS"),
                            EMAIL = fila.Field<string?>("EMAIL"),
                            IMAGEN = fila.Field<string?>("IMAGEN"),
                            id_inscripcion = fila.Field<int>("id_inscripcion"),
                            quiere_ser_capitan = fila.Field<bool>("quiere_ser_capitan"),
                            fecha_inscripcion = fila.Field<DateTime>("fecha_inscripcion"),
                            id_evento = fila.Field<int>("id_evento"),
                            IdProfesor = fila.Field<int?>("IdProfesor"),
                            fecha_evento = fila.Field<DateTime>("fecha_evento"),
                            IdEventoActividad = fila.Field<int>("IdEventoActividad"),
                            IdActividad = fila.Field<int?>("IdActividad")
                        });
                }

                return al;

            }
        }


            public Alumno GetDetails(int id)
        {
            var consulta = from datos in this.tablaAlumnos.AsEnumerable()
                           where datos.Field<int>("IDUSUARIO") == id
                           select datos;

            var row = consulta.FirstOrDefault();

            if (row != null)
            {
                Alumno detalles = new Alumno();
                detalles.IDCURSO = row.Field<int>("IDCURSO");
                detalles.NombreCurso = row.Field<string?>("NombreCurso");
                detalles.FECHAINICIO = row.Field<DateTime?>("FECHAINICIO");
                detalles.FECHAFIN = row.Field<DateTime?>("FECHAFIN");
                detalles.ACTIVO = row.Field<bool?>("ACTIVO");
                detalles.IDUSUARIO = row.Field<int>("IDUSUARIO");
                detalles.NombreUsuario = row.Field<string?>("NombreUsuario");
                detalles.APELLIDOS = row.Field<string?>("APELLIDOS");
                detalles.EMAIL = row.Field<string?>("EMAIL");
                detalles.IMAGEN = row.Field<string?>("IMAGEN");
                detalles.id_inscripcion = row.Field<int>("id_inscripcion");
                detalles.quiere_ser_capitan = row.Field<bool>("quiere_ser_capitan");
                detalles.fecha_inscripcion = row.Field<DateTime>("fecha_inscripcion");
                detalles.id_evento = row.Field<int>("id_evento");
                detalles.IdProfesor = row.Field<int?>("IdProfesor");
                detalles.fecha_evento = row.Field<DateTime>("fecha_evento");
                detalles.IdEventoActividad = row.Field<int>("IdEventoActividad");
                detalles.IdActividad = row.Field<int?>("IdActividad");

                return detalles;
            }
            else
            {
                return null;
            }
        }
    }
    }
