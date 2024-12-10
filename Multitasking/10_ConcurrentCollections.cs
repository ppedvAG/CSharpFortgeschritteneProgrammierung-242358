using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Multitasking;

public class _10_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentDictionary<string, int> dict = [];

		ConcurrentBag<int> bag = [];

		//SynchronizedCollection
		//Funktioniert besser als Bag (benötigt System.ObjectModel.Primitives)
		SynchronizedCollection<int> sync = new();
		sync.Add(10);
        Console.WriteLine(sync[0]);
    }
}