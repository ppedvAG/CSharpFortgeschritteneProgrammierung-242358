using System.Diagnostics;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region	Listentheorie
		//IEnumerable
		//Basis von allen Listentypen/Collections (List, Array, Dictionary, ...)
		//Anleitung zur Erstellung einer Liste

		List<int> x = [1, 2, 3, 4, 5];

		//Diese Variable hält keine Werte
		//Solange diese Variable nicht mit foreach, ToList, ToArray, ... angesprochen wird, werden hier keine Resourcen verwendet (RAM)
		IEnumerable<int> anleitung = x.Where(e => e % 2 == 0);

		IEnumerable<int> r = Enumerable.Range(1, (int) 1E9); //1 Mrd. * 4B = 4GB
		//IEnumerable<int> r = Enumerable.Range(1, (int) 1E9).ToList(); //1 Mrd. * 4B = 4GB
        Console.WriteLine();

		///////////////////////////////////////

		//IEnumerator
		//Ermöglicht, eine Liste zu iterieren
		//Enthält drei Komponenten:
		//- object Current: Zeiger, welcher auf das derzeitige Element zeigt
		//- bool MoveNext(): Methode, welche den Zeiger weiterbewegt
		//- void Reset(): Setzt den Zeiger auf den Anfang zurück

		List<int> ints = Enumerable.Range(0, 100).ToList();
		foreach (int i in ints)
		{
            Console.WriteLine(i);
        }

		//Enumerator per Hand benutzen
		IEnumerator<int> e = ints.GetEnumerator();
		e.MoveNext();
	start:
		Console.WriteLine(e.Current);
		bool next = e.MoveNext();
		if (next)
			goto start;
		e.Reset();
		#endregion

		#region Einfaches Linq
		List<int> zahlen = Enumerable.Range(1, 20).ToList();
        Console.WriteLine(zahlen.Average()); //Kann ohne Parameter definiert werden, oder mit einem Selektor
        Console.WriteLine(zahlen.Min());
        Console.WriteLine(zahlen.Max());
        Console.WriteLine(zahlen.Sum());

        Console.WriteLine(zahlen.First()); //Erstes Element, Exception wenn kein Element gefunden
		Console.WriteLine(zahlen.Last());

		Console.WriteLine(zahlen.FirstOrDefault()); //Erstes Element, default wenn kein Element gefunden
		Console.WriteLine(zahlen.LastOrDefault());

		//Console.WriteLine(zahlen.First(e => e % 50 == 0)); //Exception
		//Console.WriteLine(zahlen.First(TeilbarDurch50)); //Exception
		Console.WriteLine(zahlen.FirstOrDefault(e => e % 50 == 0)); //0
		#endregion

		#region	Linq mit Objekten
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//Aufgabe: Alle BMWs finden
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW);

		//Aufgabe: Alle BMWs finden, welche über 250km/h fahren können
		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.BMW)
			.Where(e => e.MaxV >= 250);

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW && e.MaxV >= 250);

		//Sortierung
		fahrzeuge.OrderBy(e => e.Marke);

		//Subsequente Sortierung nach Geschwindigkeit
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//All, Any
		fahrzeuge.All(e => e.MaxV >= 250); //Fahren alle Fahrzeuge mind. 250km/h?
		fahrzeuge.Any(e => e.MaxV >= 250); //Fährt mind. ein Fahrzeug mind. 250km/h?

		string name = "Lukas";
		if (name.All(char.IsLetter))
		{
			//...
		}

		//Count
		//Wieviele BMWs haben wir?
		int bmws = fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW);

		//Average, Min, Max, Sum, MinBy, MaxBy
		//Was ist die durchschnittliche Geschwindigkeit von allen Fahrzeugen?
		fahrzeuge.Average(e => e.MaxV); //208.41666666666666

		//Was ist die durchschnittliche Geschwindigkeit von allen Audis?
		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.Audi)
			.Average(e => e.MaxV);

		//Min, MinBy
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit

		//Skip, Take

		//Beispiel 1: Webshop
		//Seite1: 10 Fahrzeuge, Seite 2: 2 Fahrzeuge
		int page = 1;
		fahrzeuge.Skip(page * 10).Take(10);

		//Beispiel 2: Finde die 3 schnellsten Fahrzeuge
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//GroupBy
		//Bilde Gruppen anhand eines Kriteriums, und füge jedes Element zur entsprechenden Gruppe hinzu
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe

		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, v => v.ToList()); //Key-Selector, Value-Selector

		///////////////////////////////////////////////////////////////////////////////////////

		string readJson = File.ReadAllText("C:\\Users\\lk3\\source\\repos\\CSharp_Fortgeschritten_2024_12_09\\LinqErweiterungsmethoden\\history.city.list.min.json");
		JsonDocument jd = JsonDocument.Parse(readJson);

		Dictionary<string, List<JsonElement>> cities = [];
		foreach (JsonElement je in jd.RootElement.EnumerateArray()) //Json Datei iterieren
		{
			string countryCode = je.GetProperty("city").GetProperty("country").GetString(); //Auf den CountryCode zugreifen (AT, DE, IT, ...)
			if (!cities.ContainsKey(countryCode))
				cities.Add(countryCode, []);
			cities[countryCode].Add(je);
		}

		Dictionary<string, List<JsonElement>> citiesJson = jd.RootElement.EnumerateArray()
			.GroupBy(e => e.GetProperty("city", "country").GetString())
			.ToDictionary(e => e.Key, e => e.ToList());

		Console.WriteLine(cities.SequenceEqual(citiesJson));

		///////////////////////////////////////////////////////////////////////////////////////

		//Select
		//Transformation einer Liste

		//Welche Marken haben wir?
		fahrzeuge.Select(e => e.Marke); //Transformation von Fahrzeug -> FahrzeugMarke
		fahrzeuge.Select(e => e.Marke).Distinct(); //Eindeutig machen

		//Welche Fahrzeuge fahren mind. 250km/h?
		fahrzeuge.Select(e => e.MaxV >= 250); //Transformation von Fahrzeug -> bool

		//Alle Zahlen von 0 bis 10 in 0.25er Schritten
		Enumerable.Range(0, 40).Select(e => e / 4.0);

		//Ganze Liste casten
		//Boolean Liste zu 1 und 0 konvertieren
		fahrzeuge.Select(e => e.MaxV >= 250).Select(e => e == true ? 1 : 0);

		//String zu Charcodes konvertieren
		string text = "Hallo Welt";
		text.Select(e => (int) e);
		#endregion

		#region Erweiterungsmethoden
		//Alle Linq-Methoden sind Erweiterungsmethoden
		//Jede Erweiterungsmethode benötigt einen this Parameter
		int z = 2841;
		z.Quersumme();
        Console.WriteLine(z.Quersumme());

		int[] a = [1, 2, 3];
        Console.WriteLine(a.AsString());
        Console.WriteLine(fahrzeuge.AsString());

		//Compileroutput
		ExtensionMethods.Quersumme(z);
        Console.WriteLine(ExtensionMethods.AsString(a));
        #endregion
    }

	static bool TeilbarDurch50(int e)
	{
		return e % 50 == 0;
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }