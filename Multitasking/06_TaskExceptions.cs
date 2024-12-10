namespace Multitasking;

public class _06_TaskExceptions
{
    static void Main(string[] args)
    {
		Task t = new Task(Run);
		t.Start();

		try
		{
			t.Wait();
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
                Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
            }
		}

		Console.ReadKey();
    }

	static void Run()
	{
		throw new NotImplementedException("Something happened...");
	}
}