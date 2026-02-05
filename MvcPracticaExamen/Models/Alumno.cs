namespace MvcPracticaExamen.Models
{
    public class Alumno
    {
        // ===== Curso =====
        public int IDCURSO { get; set; }
        public string? NombreCurso { get; set; }
        public DateTime? FECHAINICIO { get; set; }
        public DateTime? FECHAFIN { get; set; }
        public bool? ACTIVO { get; set; }

        // ===== Usuario =====
        public int IDUSUARIO { get; set; }
        public string? NombreUsuario { get; set; }
        public string? APELLIDOS { get; set; }
        public string? EMAIL { get; set; }
        public string? IMAGEN { get; set; }

        // ===== Inscripción =====
        public int id_inscripcion { get; set; }
        public bool quiere_ser_capitan { get; set; }
        public DateTime fecha_inscripcion { get; set; }

        // ===== Evento =====
        public int id_evento { get; set; }
        public int? IdProfesor { get; set; }
        public DateTime fecha_evento { get; set; }

        // ===== Evento - Actividad =====
        public int IdEventoActividad { get; set; }
        public int? IdActividad { get; set; }
    }
}
