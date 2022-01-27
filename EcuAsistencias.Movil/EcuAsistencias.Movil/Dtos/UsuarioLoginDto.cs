
namespace EcuAsistencias.Dtos
{
	public class UsuarioLoginDto
	{
		public string Identificacion { get; set; }

		public string Contrasenia { get; set; }

	}

	public class SesionDto
	{
		public string Error { get; set; }
		public RolDto Rol { get; set; }
	}
}