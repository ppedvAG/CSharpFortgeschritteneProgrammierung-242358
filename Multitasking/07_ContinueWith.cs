namespace Multitasking;

public class _07_ContinueWith
{
    static void Main(string[] args)
    {
		//ContinueWith
		//Tasks verketten -> Wenn t1 fertig ist, t2 starten
		Task t = new Task(Run);
		t.ContinueWith(Test); //Tasks werden hier sequentiell angelegt
		t.ContinueWith(vorherigerTask =>
		{
			for (int i = 0; i < 50; i++)
			{
				Console.WriteLine($"Task 3: {i}");
			}
		}); //Beide Continuations sind parallel
		t.Start();

		t.Wait();

		/////////////////////////////////////////////////////////////

		//Beispiel: t1 -> t2 -> t3
		Task task = new Task(Run);
		task.ContinueWith(Test)
			.ContinueWith(vorherigerTask =>
			{
				for (int i = 0; i < 50; i++)
				{
					Console.WriteLine($"Task 3: {i}");
				}
			}); //Beide Continuations sind parallel
		task.Start();

		task.Wait();

		/////////////////////////////////////////////////////////////

		//Beispiel: Ergebnis vom vorherigen Task weiterverarbeiten
		Task<int> ergebnis = new Task<int>(Ergebnis);
		ergebnis.ContinueWith(t => Console.WriteLine(t.Result)); //t: Vorheriger Task (Variable: ergebnis)
		ergebnis.Start();

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
            Console.WriteLine($"Main Thread: {i}");
        }

		/////////////////////////////////////////////////////////////

		Console.Clear();

		//TaskContinuationOptions
		//ContinueWith mit Bedingungen
		Task o = new Task(RandomError);
		o.ContinueWith(t => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion);
		o.ContinueWith(t => Console.WriteLine(t.Exception.InnerException.Message), TaskContinuationOptions.OnlyOnFaulted);
		o.Start();

		Console.ReadKey();
    }

	static void Run()
	{
		for (int i = 0; i < 50; i++)
		{
            Console.WriteLine($"Task 1: {i}");
        }
	}

	static void Test(Task vorherigerTask)
	{
		for (int i = 0; i < 50; i++)
		{
			Console.WriteLine($"Task 2: {i}");
		}
	}

	static int Ergebnis()
	{
		Thread.Sleep(Random.Shared.Next(1000, 2000));
		return Random.Shared.Next();
	}

	static void RandomError()
	{
		Thread.Sleep(500);
		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("50% getroffen");
	}
}