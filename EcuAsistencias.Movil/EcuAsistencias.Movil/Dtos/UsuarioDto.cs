using System;
using System.Collections.Generic;

namespace EcuAsistencias.Dtos
{
	public class UsuarioDto
	{
		public string Identificacion { get; set; }

		public string Nombre { get; set; }

		public string Apellido { get; set; }

		public DateTime FechaNacimiento { get; set; }

		public bool Activo { get; set; }

		public DateTime HorarioInicio { get; set; }

		public DateTime HorarioFin { get; set; }

		public int IdRol { get; set; }
		public string DetalleRol { get; set; }
	}
}