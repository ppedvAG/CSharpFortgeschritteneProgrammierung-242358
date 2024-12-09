namespace Delegates;

internal class Delegate
{
	//Definition eines Delegates
	public delegate void Vorstellung(string name);

	static void Main(string[] args)
	{
		//Delegate
		//Eigener Typ, welcher Methodenzeiger halten kann
		//Hat einen Methodenaufbau (RÜckgabewert + Parameter), dieser gibt vor, welche Methoden angehängt werden können

		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit Initialmethode
		v("Max");

		//An ein bestehendes Delegate können weitere Methoden angehängt werden (mit +=)
		v += VorstellungEN;
		v("Tim");

		//Von einem Delegate können mittels -= Methoden abgehängt werden
		v -= VorstellungDE;
		v -= VorstellungDE; //Wenn ein Delegate entfernt werden soll, welches nicht angehängt ist, passiert nichts
		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE;
		v("Udo");

		//Wenn ein Delegate seine letzte Methode verliert, wird es null
		v -= VorstellungEN;
		//v("Max");

		v += VorstellungEN;

		//Lösung 1: if-Null-Check
		if (v is not null)
			v("Max");

		//Lösung 2: Null Propagation
		v?.Invoke("Max"); //Wenn v nicht null ist, wird Invoke ausgeführt, sonst nichts

		//Delegate anschauen
		foreach (System.Delegate dg in v.GetInvocationList())
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
        Console.WriteLine($"Hallo mein Name ist {name}");
    }

	static void VorstellungEN(string name)
	{
        Console.WriteLine($"Hello my name is {name}");
    }
}
