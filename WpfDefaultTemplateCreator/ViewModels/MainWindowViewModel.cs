using System;
using System.Linq;
using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Helpers;

using WpfDefaultTemplateCreator.Models;

namespace WpfDefaultTemplateCreator.ViewModels {
	internal class MainWindowViewModel {
		public IReadOnlyReactiveProperty<string> Xaml {
			get;
		}

		public IReactiveProperty<ReactivePropertySlim<Type>> SelectedControl {
			get;
		} = new ReactivePropertySlim<ReactivePropertySlim<Type>>();

		public IReactiveProperty<string> SearchWord {
			get;
		} = new ReactivePropertySlim<string>();

		public IFilteredReadOnlyObservableCollection<ReactivePropertySlim<Type>> Controls {
			get;
		}

		public MainWindowViewModel() {
			// IFilteredReadOnlyObservableCollectionを使うためにTypeをReactivePropertySlimで包んでいる。
			// 選んだ理由はINotifyPropertyChangedを実装しているからで、それ以上の意味はない。
			var rc = new ReactiveCollection<ReactivePropertySlim<Type>>();
			rc.AddRangeOnScheduler(XamlCreator.GetControlList().Select(x => new ReactivePropertySlim<Type>(x)));

			bool FilterFunc(ReactivePropertySlim<Type> x) {
				return string.IsNullOrWhiteSpace(this.SearchWord.Value) || x.Value.ToString().IndexOf(this.SearchWord.Value, StringComparison.OrdinalIgnoreCase) >= 0;
			}
			this.Controls = rc.ToFilteredReadOnlyObservableCollection(FilterFunc);
			this.SearchWord.Subscribe(_ => {
				this.Controls.Refresh(FilterFunc);
			});
			this.Xaml = this.SelectedControl.Select(x => XamlCreator.Create(x?.Value)).ToReadOnlyReactivePropertySlim();
		}
	}
}
