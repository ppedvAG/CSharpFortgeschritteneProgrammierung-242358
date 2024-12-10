namespace Multitasking;

public class _02_TaskMitReturn
{
    static void Main(string[] args)
    {
		Task<int> t = new Task<int>(Run); //Über Generic den Rückgabetyp festlegen
		t.Start();
		
		Console.WriteLine(t.Result);
		//Problem: Blockieren des Main Threads
		//Wir wollen das Ergebnis haben, sobald es fertig ist
		//LÖsung: ContinueWith, await

		//Ab hier wird der Code gestoppt, bis das Result gekommen ist

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}
	}

	static int Run()
	{
		Thread.Sleep(1000);
		return Random.Shared.Next();
	}
}