using EcuAsistencias.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class LoginService
	{
		public static readonly string Controller = "Login";
		public static async Task<SesionDto> GetSesionAsync(UsuarioLoginDto login)
		{
			return await HttpService.GetApiFromObject<SesionDto>(Controller, login);
		}
	}
}
