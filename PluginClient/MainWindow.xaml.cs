using System.Reflection;
using System.Windows;
using Microsoft.Win32;
using PluginBase;

namespace PluginClient;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	/// <summary>
	/// DLLs laden mittels Öffnen-Dialog
	/// </summary>
	private void Button_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog d = new OpenFileDialog();
		bool ok = d.ShowDialog() == true;

		if (!ok)
			return;

		//Ab hier Plugin laden
		string pfad = d.FileName;
		Assembly a = Assembly.LoadFrom(pfad);
		Type pt = a.GetTypes().FirstOrDefault(e => e.GetInterface(nameof(IPlugin)) != null);

		if (pt == null)
		{
			Output.Text = "Kein Plugin gefunden";
			return;
		}

		IPlugin plugin = Activator.CreateInstance(pt) as IPlugin;

		foreach (PropertyInfo prop in pt.GetProperties())
		{
			Output.Text += $"{prop.GetValue(plugin)}\n";
		}

		Output.Text += "----------------------\n";

		foreach (MethodInfo mi in pt.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null))
		{
			Output.Text += $"{mi.ReturnType.Name} {mi.Name}\n";
		}
	}
}