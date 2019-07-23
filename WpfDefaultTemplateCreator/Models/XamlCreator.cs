using System;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace WpfDefaultTemplateCreator.Models {
	internal static class XamlCreator {
		public static string Create(Type type) {
			var control = Application.Current.FindResource(type);
			if (control == null) {
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
	}
}
