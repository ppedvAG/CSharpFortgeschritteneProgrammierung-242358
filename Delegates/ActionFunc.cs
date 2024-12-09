using System;

namespace Delegates;

public class ActionFunc
{
	static void Main(string[] args)
	{
		//Action & Func
		//Vorgegebene Delegates, welche an vielen Stellen in C# eingesetzt sind
		//Essentiell für die fortgeschrittene Programmierung mit C#
		//Verwendet in: Linq, TPL, Multithreading, Reflection, ...

		//Action: Methodenzeiger mit void und bis zu 16 Parametern
		Action<int> a = Test;
		a(10);
		a?.Invoke(10);

		//Beispiel für Action: ForEach
		List<int> b = Enumerable.Range(1, 20).ToList();
		b.ForEach(Test); //Führt für jedes Element den Funktionszeiger aus

		List<string> c = ["Max", "Udo", "Tim"];
		c.ForEach(Test);

		/////////////////////////////////////////////

		//Func: Methodenzeiger mit T als Rückgabewert und bis zu 16 Parametern
		Func<int, int, double> f = Dividiere; //Letzter Parameter ist immer der Rückgabewert
		double d = f(8, 5);
        Console.WriteLine(d);

		//Problem: ?.Invoke gibt null zurück, wenn die Func (f) null ist
		double? d2 = f?.Invoke(8, 5);

		double d3 = 0;
		if (f != null)
			d3 = f.Invoke(8, 5);
		else
			d3 = 0;

		double d4 = f?.Invoke(8, 5) ?? 0;

		//Beispiel für Func: Where
		//Aufgabe: Alle Zahlen finden, welche durch 3 teilbar sind
		b.Where(Durch3Teilbar); //Funktion wird auf jedes Element angewandt, jedes Element welches true zurückgibt, kommt am Ende heraus

		/////////////////////////////////////////////

		//Anonyme Methode
		//Methode, welche nicht separat erzeugt wird, sondern nur einmal innerhalb einer anderen Methode verwendet wird

		f += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		f += (int x, int y) => { return x + y; }; //Kürzere Form

		f += (x, y) => { return x - y; };

		f += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anonyme Methoden als Parameter für Delegates (Action, Func)
		b.Where(e => e % 3 == 0); //Aufbau: (P1, P2, ...) => [Body]
		b.ForEach(e => Console.WriteLine($"Die Zahl mal 2 ist {e * 2}"));

		//Überall wo sich ein Delegate befindet, kann eine anonyme Funktion eingesetzt werden
	}

	#region Action
	static void Test(int a) => Console.WriteLine($"Die Zahl mal 2 ist {a * 2}");

	static void Test(string s) => Console.WriteLine($"Hallo {s}");
	#endregion

	#region Func
	static double Dividiere(int x, int y) => (double) x / y;

	static bool Durch3Teilbar(int x) => x % 3 == 0;
	#endregion
}