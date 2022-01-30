using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Services
{
	public class SistemaService
	{
		static readonly string Controller = "Sistema";
		public static async Task<SistemaDto> Get()
		{
			return await HttpService.GetApiById<SistemaDto, int>(Controller, 1);
		}

	}
}
