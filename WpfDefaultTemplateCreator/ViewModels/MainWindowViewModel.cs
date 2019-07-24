using System;
using System.Reactive.Linq;

using Reactive.Bindings;

using WpfDefaultTemplateCreator.Models;

namespace WpfDefaultTemplateCreator.ViewModels {
	internal class MainWindowViewModel {
		public IReadOnlyReactiveProperty<string> Xaml {
			get;
		}

		public IReactiveProperty<Type> SelectedControl {
			get;
		} = new ReactivePropertySlim<Type>();

		public Type[] Controls {
			get;
		}

		public MainWindowViewModel() {
			this.Controls = XamlCreator.GetControlList();
			this.Xaml = this.SelectedControl.Select(XamlCreator.Create).ToReadOnlyReactivePropertySlim();
		}
	}
}
