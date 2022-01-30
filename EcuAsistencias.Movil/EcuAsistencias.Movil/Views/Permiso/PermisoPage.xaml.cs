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

namespace EcuAsistencias.Movil.Views.Permiso
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PermisoPage : ContentPage
	{
		public class TiempoDuracionItem
		{
			public int Horas { get; set; }
			public int Minutos { get; set; }
			public string Nombre => Horas.ToString().PadLeft(2, '0') + ":" + Minutos.ToString().PadLeft(2, '0') + "h";
		}

		public PermisoPage()
		{
			InitializeComponent();

			CargarPantalla();
		}

		private async void CargarPantalla()
		{
			txtError.IsVisible = false;
			txtMotivoOtros.IsVisible = false;
			btnEnviar.IsEnabled = false;

			await CargarComboDuracion();
			await CargarComboMotivo();

			if(AppShell.Asistencia is null)
			{
				txtError.IsVisible = true;
				txtError.Text = "Solo se puede pedir permiso con una asistencia activa";
				return;
			}

			List<PermisoSalidaDto> permisos = (await PermisoSalidaService.GetAllPermisosSalidaAsync()).Where(p => p.IdAsistencia == AppShell.Asistencia.Id).ToList();
			if(permisos.Count > 0)
			{
				txtError.IsVisible = true;
				txtError.Text = "Solo puede pedir un permiso al dia";
				return;
			}

			btnEnviar.IsEnabled = true;
		}

		private async Task CargarComboDuracion()
		{
			await Task.CompletedTask;

			List<TiempoDuracionItem> tiempos = new List<TiempoDuracionItem>
			{
				new TiempoDuracionItem {Horas = 0, Minutos = 30},
				new TiempoDuracionItem {Horas = 1, Minutos = 0},
				new TiempoDuracionItem {Horas = 1, Minutos = 30},
				new TiempoDuracionItem {Horas = 2, Minutos = 0},
				new TiempoDuracionItem {Horas = 2, Minutos = 30},
				new TiempoDuracionItem {Horas = 3, Minutos = 0},
				new TiempoDuracionItem {Horas = 3, Minutos = 30},
				new TiempoDuracionItem {Horas = 4, Minutos = 0},
			};

			cmbTiempoDuracion.ItemsSource = tiempos;
		}

		private async Task CargarComboMotivo()
		{
			cmbMotivo.ItemsSource = await MotivoService.GetAllMotivosAsync();
			cmbMotivo.SelectedIndexChanged += CmbMotivo_SelectedIndexChanged;
		}

		private void CmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				MotivoDto motivo = (MotivoDto)picker.ItemsSource[selectedIndex];
				if(motivo.EsOtro)
					txtMotivoOtros.IsVisible = true;
				else
					txtMotivoOtros.IsVisible = false;
			}
		}

		private async void btnEnviar_Clicked(object sender, EventArgs e)
		{
			MotivoDto motivo = cmbMotivo.SelectedItem as MotivoDto;
			if(motivo == null)
			{
				Error("Debe seleccionar un motivo");
				return;
			}

			TiempoDuracionItem duracion = cmbTiempoDuracion.SelectedItem as TiempoDuracionItem;
			if (duracion == null)
			{
				Error("Debe seleccionar una duracion");
				return;
			}

			if(motivo.EsOtro && string.IsNullOrEmpty(txtMotivoOtros.Text))
			{
				Error("Debe escribir su motivo");
				return;
			}

			PermisoSalidaDto permiso = new PermisoSalidaDto
			{
				IdAsistencia = AppShell.Asistencia.Id,
				IdMotivo = motivo.Id,
				MotivoOtros = motivo.EsOtro ? txtMotivoOtros.Text : "",
				HoraPermiso = DateTime.Today + dteHora.Time,
				TiempoPermisoHoras = new TimeSpan(duracion.Horas, duracion.Minutos, 0)
			};

			await PermisoSalidaService.GuardarAsync(permiso);

		}

		private void Error(string error)
		{
			txtError.Text = error;
			txtError.IsVisible = true;
		}
	}
}