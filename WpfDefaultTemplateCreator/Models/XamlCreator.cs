using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace WpfDefaultTemplateCreator.Models {
	internal static class XamlCreator {
		public static string Create(Type type) {
			object control;
			try {
				control = Application.Current.FindResource(type);
			} catch (ResourceReferenceKeyNotFoundException) {
				return null;
			}

			var sb = new StringBuilder();
			var xws = new XmlWriterSettings {
				Indent = true,
				Encoding = Encoding.UTF8,
				IndentChars = "\t"
			};
			using var xtw = XmlWriter.Create(sb, xws);
			XamlWriter.Save(control, xtw);
			return sb.ToString();
		}

		public static Type[] GetControlList() {
			return
				AppDomain
					.CurrentDomain
					.GetAssemblies()
					.SelectMany(x => x.GetTypes())
					.Where(x => x.IsSubclassOf(typeof(FrameworkElement)))
					.ToArray();
		}
	}
}
