using System;

namespace EcuAsistencias.Dtos
{
	public class PermisoSalidaDto
    {
        public int Id { get; set; }

        public int IdAsistencia { get; set; }

        public DateTime HoraPermiso { get; set; }

        public TimeSpan TiempoPermisoHoras { get; set; }

        public int IdMotivo { get; set; }

        public string MotivoOtros { get; set; }
    }
}