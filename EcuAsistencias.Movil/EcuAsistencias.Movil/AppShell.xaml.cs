using EcuAsistencias.Dtos;
using EcuAsistencias.Movil.ViewModels;
using EcuAsistencias.Movil.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EcuAsistencias.Movil
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public static UsuarioLoginDto Sesion { get; set; } = null;
		public static AsistenciaDto Asistencia { get; set; } = null;

		public AppShell()
		{
			InitializeComponent();
			//Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			//Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
		}

	}
}
