using System.Collections.Concurrent;

namespace Multitasking;

public class _09_Lock
{
	public static readonly object Lock = new object();

	public static int Counter;

	static void Main(string[] args)
	{
		for (int i = 0; i < 50; i++)
			Task.Run(CounterIncrement);
		Console.ReadKey();
	}

	public static void CounterIncrement()
	{
		for (int i = 0; i < 100; i++)
		{
			//lock (Lock)
			//{
			//	Counter++;
			//	Console.WriteLine(Counter); //Outputs sind irregulär (Zahlen an der falschen Stelle)
			//}

			////Selber Effekt wie Lock, aber flexibler
			//Monitor.Enter(Lock);
			//Counter++;
			//Console.WriteLine(Counter); //Outputs sind irregulär (Zahlen an der falschen Stelle)
			//Monitor.Exit(Lock);

			//////////////////////////////////////////////////////////

			//Liste von Methoden, welche im Hintergrund automatisch Lock machen
			Interlocked.Add(ref Counter, 1);
			lock (Lock)
				Console.WriteLine(Counter);
			//Interlocked.Increment(ref Counter);
		}
	}
}