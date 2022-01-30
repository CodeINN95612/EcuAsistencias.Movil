using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcuAsistencias.Movil.Views.Asistencia
{
	public class AsistenciaItem
	{
		public string NombreCompleto { get; set; }
		public bool Asiste { get; set; }
	}

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AsistenciaLIstaPage : ContentPage
	{
		public AsistenciaLIstaPage()
		{
			InitializeComponent();

			CargarPantalla();
		}

		private async void CargarPantalla()
		{
			List<AsistenciaItem> itemsAsistencia = new List<AsistenciaItem>();

			List<UsuarioDto> usuarios = (await UsuarioService.GetAllUsuariosAsync()).Where(p => p.Activo).ToList();
			List<AsistenciaDto> asistencias = await AsistenciaService.GetAllAsistenciasAsync();
			
			DateTime date = asistencias[0].Fecha.Date;
			DateTime date2 = DateTime.Today.Date;
			bool iwal = date == date2;
			
			asistencias = asistencias.Where(p => p.Fecha.Date == DateTime.Today.Date).ToList(); //xd

			foreach (UsuarioDto usuario in usuarios)
			{
				AsistenciaDto asistio = asistencias.FirstOrDefault(p => p.IdentificacionUsuario == usuario.Identificacion);

				itemsAsistencia.Add(new AsistenciaItem
				{
					Asiste = asistio != null,
					NombreCompleto = usuario.Nombre + " " + usuario.Apellido
				});
			}

			lista.ItemsSource = itemsAsistencia;
		}
	}
}