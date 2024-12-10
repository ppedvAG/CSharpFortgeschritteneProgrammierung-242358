namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Task t2 = Task.Factory.StartNew(Run); //Ab .NET 4.0
		Task t3 = Task.Run(Run); //Ab .NET 4.5

		//Task wird hier vorzeitig abgebrochen
		//Hier muss der Main Thread geblockt werden
		//Beispiele: Console.ReadKey(), t.Wait(), ...
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 1000; i++)
            Console.WriteLine($"Task: {i}");
    }
}