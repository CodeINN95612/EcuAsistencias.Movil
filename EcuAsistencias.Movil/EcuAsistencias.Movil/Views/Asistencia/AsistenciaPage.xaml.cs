using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Dtos;
using EcuAsistencias.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcuAsistencias.Movil.Views.Asistencia
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AsistenciaPage : ContentPage
	{
		private UsuarioDto _UsuarioActual = new UsuarioDto();
		private SistemaDto _InformacionSistema = new SistemaDto();

		public AsistenciaPage()
		{
			InitializeComponent();

			CargarPantalla();
		}

		private async void CargarPantalla()
		{

			UsuarioLoginDto sesionActual = AppShell.Sesion;

			_UsuarioActual = await UsuarioService.GetUsuarioAsync(sesionActual.Identificacion);
			_InformacionSistema = await SistemaService.Get();

			var asistencias = await AsistenciaService.GetAllAsistenciasAsync();

			AsistenciaDto asistenciaActual = asistencias.FirstOrDefault(p => p.IdentificacionUsuario.Equals(_UsuarioActual.Identificacion) && p.Fecha.Date == _InformacionSistema.FechaSistema.Date);
			btnIngresar.IsEnabled = asistenciaActual == null;
			btnSalir.IsEnabled = asistenciaActual != null;// && asistenciaActual.Salida != null;

			if(asistenciaActual != null)
			{
				AppShell.Asistencia = asistenciaActual;
			}

			dteTiempo.Time = _InformacionSistema.HoraSistema;
		}

		private async void btnIngresar_Clicked(object sender, EventArgs e)
		{
			_InformacionSistema = await SistemaService.Get();

			AsistenciaDto asistenciaNueva = new AsistenciaDto
			{
				Fecha = _InformacionSistema.FechaSistema,
				IdentificacionUsuario = _UsuarioActual.Identificacion,
				Ingreso = _InformacionSistema.FechaSistema,
				Salida = null,
			};

			AppShell.Asistencia = await AsistenciaService.GuardarAsync(asistenciaNueva);

			btnIngresar.IsEnabled = false;
			btnSalir.IsEnabled = true;
		}

		private async void btnSalir_Clicked(object sender, EventArgs e)
		{
			_InformacionSistema = await SistemaService.Get();

			if (AppShell.Asistencia != null)
			{
				AppShell.Asistencia.Salida = _InformacionSistema.FechaSistema;
				await AsistenciaService.GuardarAsync(AppShell.Asistencia);
			}

			AppShell.Asistencia = null;
			btnSalir.IsEnabled = false;
		}

	}
}