using System.Collections;

namespace Generic;

internal class Program
{
	static void Main(string[] args)
	{
		//Generic: Platzhalter für einen konkreten Typen
		//Wird innerhalb der Klasse/Methode ausgetauscht durch den gegebenen Typen

		//Beispiel: List<T>
		List<int> ints = []; //Überall in der List Klasse, wird T durch int ersetzt
		ints.Add(10); //Add(T item) -> Add(int item)

		List<string> strings = [];
		strings.Add("A"); //Add(T item) -> Add(string item)

		Dictionary<string, int> dict = [];
		dict.Add("B", 2);
    }

	static void Test<T>()
	{

	}
}

public class DataStore<T> : IEnumerable<T> //T bei Vererbung
{
	private T[] _data { get; set; } //T bei Feld

	public List<T> Data => _data.ToList(); //T bei einem anderen Generic

	public void Add(T item, int index) //T als Parameter
	{
		_data[index] = item;
	}

	public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

	IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

	public T Get(int index) => _data[index]; //T bei Rückgabewert

	////////////////////////////////////////////////
	
	public void Test()
	{
        Console.WriteLine(default(T)); //Standardwert von T
        Console.WriteLine(nameof(T)); //Stringrepräsentation von T
        Console.WriteLine(typeof(T)); //Typ hinter T
    }
}