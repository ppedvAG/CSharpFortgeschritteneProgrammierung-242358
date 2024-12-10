namespace Multitasking;

public class _03_TaskWarten
{
    static void Main(string[] args)
    {
		Task t1 = new Task(Run);
		t1.Start();

		t1.Wait(); //Ab hier wird der Main Thread blockiert

		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		Task.WaitAll(t1, t2, t3); //Auf mehrere Tasks warten
		Task.WaitAny(t1, t2, t3); //Auf einen der Tasks warten, Rückgabewert (int) beschreibt den Index des schnellsten Tasks
    }

	static void Run()
	{
		for (int i = 0; i < 100; i++) 
		{
            Console.WriteLine($"Task: {i}");
        }
	}
}