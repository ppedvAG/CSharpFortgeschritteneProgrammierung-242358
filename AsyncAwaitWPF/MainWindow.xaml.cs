using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Info.Text += i + "\n";
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() =>
				{
					Info.Text += i + "\n";
					Scroll.ScrollToEnd();
				});
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Info.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource a = new();
		await foreach (int x in a.GetNumbers()) //await foreach: Warte immer auf das nächste Element
		{
			Info.Text += x + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//Aufbau:
		//- Start der Aufgabe (Task t = ...)
		//- Zwischenschritte
		//- Auf Aufgabe warten mit await (await t)

		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		using HttpClient client = new HttpClient();

		//Aufgabe starten
		Task<HttpResponseMessage> request = client.GetAsync(url);
		
		//Zwischenschritte
		Info.Text = "Text wird geladen...";
		ReqButton.IsEnabled = false;

		//Warten
		HttpResponseMessage resp = await request;

		if (resp.IsSuccessStatusCode)
		{
			Task<string> readTask = resp.Content.ReadAsStringAsync();

			Info.Text = "Text wird ausgelesen...";

			string str = await readTask;

			Info.Text = str;

			ReqButton.IsEnabled = true;
		}
	}
}