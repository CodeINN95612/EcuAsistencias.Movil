using EcuAsistencias.Core.Servicios;
using EcuAsistencias.Dtos;
using EcuAsistencias.Movil.Views;
using EcuAsistencias.Movil.Views.Asistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EcuAsistencias.Movil.ViewModels
{
	public class LoginViewModel
	{
		public Command LoginCommand { get; }
		public UsuarioLoginDto Login { get; set; }

		private Label _LabelError;
		private ActivityIndicator _Progreso;

		public LoginViewModel(Label error, ActivityIndicator progreso)
		{
			LoginCommand = new Command(OnLoginClicked);
			Login = new UsuarioLoginDto()
			{
				Contrasenia = string.Empty,
				Identificacion = string.Empty,
			};
			_LabelError = error;
			_Progreso = progreso;
		}

		private async void OnLoginClicked(object obj)
		{
			_Progreso.IsRunning = true;

			SesionDto sesion = await LoginService.GetSesionAsync(Login);

			if (sesion.Error != null)
			{
				_LabelError.Text = sesion.Error;
				_LabelError.IsVisible = true;
			}
			else
			{
				AppShell.Sesion = Login;
				await Shell.Current.GoToAsync($"//{nameof(AsistenciaPage)}");
			}

			_Progreso.IsRunning = false;
		}
	}
}
