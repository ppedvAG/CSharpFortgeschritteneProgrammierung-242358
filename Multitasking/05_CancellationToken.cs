namespace Multitasking;

public class _05_CancellationToken
{
    static void Main(string[] args)
    {
		//CancellationToken
		//Allgemeines System, welches für das Abbrechen von länger andauernden Aufgaben verantwortlich ist

		//Zwei Teile
		//- Source, produziert Token
		//- Token selbst

		CancellationTokenSource cts = new();
		CancellationToken token = cts.Token;

		Task t = new Task(Run, token); //Hier wird eine Kopie des Tokens angelegt (struct)
		t.Start();

		Thread.Sleep(500);

		cts.Cancel(); //Auf der Source werden alle Tokens abgebrochen

		Console.ReadKey();
    }

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Wenn ein Task eine Exception wirft, bekommen wir die Exception nicht mit

				//LÖsungen: AggregateException, ContinueWith, t.Status

				Thread.Sleep(25);
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}