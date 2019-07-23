using System.Windows.Controls;

using WpfDefaultTemplateCreator.Models;

namespace WpfDefaultTemplateCreator.ViewModels {
	internal class MainWindowViewModel {
		public string Xaml {
			get {
				return XamlCreator.Create(typeof(GridViewColumnHeader));
			}
		}
	}
}
