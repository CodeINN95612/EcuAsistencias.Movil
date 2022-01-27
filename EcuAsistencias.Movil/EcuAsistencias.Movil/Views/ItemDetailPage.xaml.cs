using EcuAsistencias.Movil.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EcuAsistencias.Movil.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}