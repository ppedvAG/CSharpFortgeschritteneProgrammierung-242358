namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		//int zahl;
		bool b = int.TryParse("-1", out int zahl); //Vereinfachung
		if (b)
		{
			Console.WriteLine("Funktioniert");
		}
		Console.WriteLine(zahl);

		////////////////////////////////////////

		object o = new FileStream(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_12_09\README.md", FileMode.Open);
		if (o.GetType() == typeof(Stream)) { } //Genauer Typvergleich

		//Vererbungshierarchie wird hier beachtet
		if (o is Stream)
		{
			Stream s1 = (Stream) o; //Wird eigentlich immer gemacht
		}

		if (o is Stream s2)
		{
			//Stream s2 = (Stream) o; //Automatisch
		}

		if (o is not null) { }
		if (o != null) { }

		/////////////////////////////////////////////

		switch (o) //WICHTIG: Nur Objekt selbst in den switch geben
		{
			case int:
				Console.WriteLine();
				break;
			case float:
				Console.WriteLine();
				break;
			case double:
				Console.WriteLine();
				break;
			case string:
				Console.WriteLine();
				break;
		}

		/////////////////////////////////////////////

		//Tupel: Liste von BENANNTEN Elementen
		(int x, int y) = (1, 3);
		int[] xy = [1, 3];

		Console.WriteLine(x);
		Console.WriteLine(y);

		Console.WriteLine(xy[0]);
		Console.WriteLine(xy[1]);

		TryParse("123");

		Person p = new Person() { Id = 10, Name = "Max" };
		(int id, string name) = p;
		Console.WriteLine(id);
		Console.WriteLine(name);

		/////////////////////////////////////////////

		void Lokal()
		{

		}

		Lokal();

		/////////////////////////////////////////////

		int t = 123_456_789;
		Console.WriteLine(t);

		/////////////////////////////////////////////

		//struct
		//Wertetyp
		//Beispiele: int, float, double, bool, ...
		//Erzeugt eine Kopie von sich selbst, wenn es zugewiesen wird
		//Bei Vergleichen werden die Inhalte verglichen
		int a = 10;
		int c = a;
		c = 20; //a = 10, c = 20

		//class
		//Referenztyp
		//Beispiele: FileStream, Program, Person, ...
		//Erzeugt eine Referenz auf das Objekt, wenn es zugewiesen wird
		//Bei Vergleichen werden die Speicheradressen verglichen
		Person p1 = new Person() { Id = 10 };
		Person p2 = p1; //Hier wird eine Referenz auf das Objekt unter p1 gelegt
		p2.Id = 20;

		Console.WriteLine(p1.GetHashCode());
		Console.WriteLine(p2.GetHashCode());

		if (p1 == p2) { }

		if (p1.GetHashCode() == p2.GetHashCode()) { }

		//ref
		//Beliebige Typen als Referenz behandeln
		int a1 = 10;
		ref int c1 = ref a1;
		c1 = 20;
    }

	static (bool funktioniert, int ergebnis) TryParse(string str)
	{
		try
		{
			return (true, int.Parse(str));
		}
		catch
		{
			return (false, 0);
		}
	}

	static void Test() => Console.WriteLine("Hallo Welt");
}

class Person
{
	private int id;

	public int Id
	{
		get => id;
		set => id = value;
	}

	public string Name { get; set; }

	public void Deconstruct(out int id, out string name)
	{
		id = Id;
		name = Name;
	}
}