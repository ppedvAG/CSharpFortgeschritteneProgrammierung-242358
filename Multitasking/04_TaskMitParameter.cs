namespace Multitasking;

public class _04_TaskMitParameter
{
    static void Main(string[] args)
    {
		Task t = new Task(Run, 400);
		t.Start();

		Console.ReadKey();
    }

	static void Run(object? o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}