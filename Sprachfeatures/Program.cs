using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal unsafe class Program
{
	static unsafe void Main(string[] args)
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

		/////////////////////////////////////////////

		string typ2;
		switch (o)
		{
			case int:
				typ2 = "Integer";
				break;
			case string:
				typ2 = "String";
				break;
			case double:
				typ2 = "Double";
				break;
			default:
				typ2 = "Anderer Typ";
				break;
		}

		string typ = o switch
		{
			int => "Integer",
			string => "String",
			double => "Double",
			_ => "Anderer Typ"
		};

		int id2 = p switch
		{
			{ Id: 10 } => 10,
			{ Name: "Max", Id: 11 } => 20,
			_ => 0
		};

		//Position-Pattern: Benötigt die Deconstruct Methode
		int id3 = p switch
		{
			var (g, h) when id == 10 && name == "Max" => 10 
		};

		/////////////////////////////////////////////

		using StreamWriter sw = new StreamWriter(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_12_09\FUNDING.yml");
		using HttpClient http = new HttpClient();
		//using DbConnection conn = new DbConnection(sw);

		/////////////////////////////////////////////
		
		static void LokalTest()
		{
			//a = 50; //Mit static nicht möglich
		}

		/////////////////////////////////////////////

		string str = "Hallo";
		if (str != null)
            Console.WriteLine(str);
		else
            Console.WriteLine("string ist leer");

        Console.WriteLine(str != null ? str : "string ist leer");

        Console.WriteLine(str ?? "string ist leer"); //Wenn die linke Seite nicht null ist, nimm die Linke Seite, sonst die rechte Seite

		/////////////////////////////////////////////

		//String-Interpolation ($-String): Code in einen String einbauen

		//Aufgabe: id, name, a in einer schönen Form ausgeben
		Console.WriteLine("ID: " + id + ", Name: " + name + ", a: " + a);

        Console.WriteLine($"ID: {id}, Name: {name}, a: {a}");

		Console.WriteLine($"Der Typ von o: {o switch
		{
			int => "Integer",
			string => "String",
			double => "Double",
			_ => "Anderer Typ"
		}}");

		Console.WriteLine($"Parse: {int.Parse("123")}");

		Console.WriteLine($"Die Person: {new Person()}");

		/////////////////////////////////////////////

		Person p3 = null;
        //Console.WriteLine(p3.Id);

		/////////////////////////////////////////////

		var d1 = new Dictionary<string, string>();
		Dictionary<string, string> d2 = new();

		Person2 person2 = new Person2(10, "Max");
		Person2 person3 = new Person2(10, "Max");
        Console.WriteLine(person2 == person3);

        /////////////////////////////////////////////

        Console.WriteLine($"""Hallo "Welt" """);
		Console.WriteLine($$"""   {{{a}}}   """);
		Console.WriteLine($"      {{{a}}}     ");

		/////////////////////////////////////////////

		List<int> list;
		IntList list2;

		/////////////////////////////////////////////
		
		DateTime dt = DateTime.Now;
		dt += TimeSpan.FromDays(3);
		dt -= TimeSpan.FromDays(3);

		int i = 10;
		double d = i;

		/////////////////////////////////////////////

		string str1 = "Hallo"; //Hallo wird im RAM abgelegt
		string str2 = "Welt"; //Welt wird im RAM abgelegt
		str1 += str2; //Kopie von str1 angelegt, str2 wird angehängt

		string ergebnis = "";
		for (int j = 0; j < 100; j++)
		{
			ergebnis += j;
			Console.WriteLine(ergebnis);
		}

		StringBuilder sb = new();
		for (int j = 0; j < 100; j++)
		{
			sb.Append(j);
		}
		Console.WriteLine(sb.ToString());
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

	public string Heute() => DateTime.Now.DayOfWeek switch
	{
		DayOfWeek.Monday => "Montag",
		DayOfWeek.Tuesday => "Dienstag",
		_ => "Anderer Tag"
	};
}

/// <summary>
/// Primary Constructor
/// 
/// Erzeugt automatisch den Konstruktor und die private Felder die von dem Konstruktor kommen
/// </summary>
class Person//(int id, string name)
{
	private int id;

	private string name;

	public int Id
	{
		get => id;
		set => id = value;
	}

	public string Name
	{
		get => name;
		set => name = value;
	}

	public void Deconstruct(out int id, out string name)
	{
		id = Id;
		name = Name;
	}

	//public Person(int id, string name)
	//{
	//	this.id = id;
	//	this.Name = name;
	//}
}

public class AccessModifier
{
	public int Alter { get; set; } //public: Kann von überall angegriffen werden (auch außerhalb vom Projekt)

	internal string Vorname { get; set; } //internal: Kann von überall angegriffen werden, aber nicht außerhalb vom Projekt

	private string Nachname { get; set; } //private: Kann nur innerhalb der Klasse selbst angegriffen werden

	//////////////////////////////////////////////////////

	protected string Adresse { get; set; } //protected: Wie private, aber auch in Unterklassen sichtbar (auch außerhalb)

	protected private string Haarfarbe { get; set; } //protected private: Wie protected, aber nur im Projekt verwendbar

	protected internal int Groesse { get; set; } //protected internal: Im gesamten Projekt sichtbar (wie internal), aber auch in Unterklassen außerhalb
}

public record Person2(int ID, string Vorname)
{
	public void Test()
	{

	}
}