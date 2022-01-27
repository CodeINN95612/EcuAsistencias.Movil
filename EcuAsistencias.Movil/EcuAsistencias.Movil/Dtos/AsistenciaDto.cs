using System;
using System.Collections.Generic;

namespace EcuAsistencias.Dtos
{
	public class AsistenciaDto
	{
		public int Id { get; set; }

		public string IdentificacionUsuario { get; set; }

		public DateTime Fecha { get; set; }

		public DateTime Ingreso { get; set; }

		public DateTime? Salida { get; set; }

	}
}