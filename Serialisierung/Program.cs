using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//File, Directory, Path

		//Aufgabe: Ordner erstellen
		if (!Directory.Exists("Test"))
			Directory.CreateDirectory("Test");

		//Aufgabe: Text in ein File in dem Unterordner schreiben
		string filePath = Path.Combine("Test", "Test.txt");
		//File.WriteAllText(filePath, "Hallo Welt");

		///////////////////////////////////////////////////

		//Newtonsoft Json

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new PKWRot(274, FahrzeugMarke.BMW),
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

		//XML
		//1. De-/Serialisieren
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			xml.Serialize(fs, fahrzeuge);
		} //.Close()

		using (FileStream fs = new FileStream(filePath, FileMode.Open))
		{
			List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(fs);
		}

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Attributschreibweise für einzelne Felder erzwingen
		//XmlInclude: Vererbung in XML

		//3. XML per Hand
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);
		foreach (XmlNode element in doc.DocumentElement)
		{
			Console.WriteLine(element["MaxV"].Value);
			Console.WriteLine(Enum.Parse<FahrzeugMarke>(element["Marke"].InnerText));
			Console.WriteLine("------------------------");

			//Sobald ein BMW gefunden wurde, stoppen
			FahrzeugMarke current = Enum.Parse<FahrzeugMarke>(element["Marke"].InnerText);
			if (current == FahrzeugMarke.BMW)
				return;
		}
	}

	static void SystemJson()
	{
		//File, Directory, Path

		//Aufgabe: Ordner erstellen
		if (!Directory.Exists("Test"))
			Directory.CreateDirectory("Test");

		//Aufgabe: Text in ein File in dem Unterordner schreiben
		string filePath = Path.Combine("Test", "Test.txt");
		//File.WriteAllText(filePath, "Hallo Welt");

		///////////////////////////////////////////////////
		
		//System.Text.Json

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new PKWRot(274, FahrzeugMarke.BMW),
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

		////1. De-/Serialisieren
		//string json = JsonSerializer.Serialize(fahrzeuge);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> fzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson); //Über das Generic wird der Typ des Outputs bestimmt

		////2. Options
		//JsonSerializerOptions options = new(); //Verschiedene Einstellungen vornehmen
		//options.WriteIndented = true; //Json schön schreiben

		//File.WriteAllText(filePath, JsonSerializer.Serialize(fahrzeuge, options));

		////3. Attribute
		////JsonIgnore
		////JsonRequired: Feld muss beim de-/serialisieren einen Wert haben
		////JsonExtensionData: Fängt Felder, welche beim deserialisieren kein entsprechendes Feld in der C# Klasse haben
		////JsonConstructor: Gibt an, welcher Konstruktor beim deserialisieren verwendet werden soll
		////JsonDerivedType: Vererbung serialisieren (benötigt ein Attribut pro Unterklasse)

		////4. Json per Hand durchgehen
		//JsonDocument doc = JsonDocument.Parse(readJson);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	Console.WriteLine(element.GetProperty("MaxV").GetInt32());
		//	Console.WriteLine((FahrzeugMarke) element.GetProperty("Marke").GetInt32());
		//	Console.WriteLine("------------------------");

		//	//Sobald ein BMW gefunden wurde, stoppen
		//	FahrzeugMarke current = (FahrzeugMarke) element.GetProperty("Marke").GetInt32();
		//	if (current == FahrzeugMarke.BMW)
		//		return;
		//}
	}

	static void NewtonsoftJson()
	{
		//File, Directory, Path

		//Aufgabe: Ordner erstellen
		if (!Directory.Exists("Test"))
			Directory.CreateDirectory("Test");

		//Aufgabe: Text in ein File in dem Unterordner schreiben
		string filePath = Path.Combine("Test", "Test.txt");
		//File.WriteAllText(filePath, "Hallo Welt");

		///////////////////////////////////////////////////

		//Newtonsoft Json

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new PKWRot(274, FahrzeugMarke.BMW),
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

		//1. De-/Serialisieren
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> fzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson); //Über das Generic wird der Typ des Outputs bestimmt

		//2. Options
		JsonSerializerSettings options = new(); //Verschiedene Einstellungen vornehmen
		//options.Formatting = Formatting.Indented; //Json schön schreiben
		options.TypeNameHandling = TypeNameHandling.Objects; //Vererbung

		File.WriteAllText(filePath, JsonConvert.SerializeObject(fahrzeuge, options));

		//3. Attribute
		//JsonIgnore
		//JsonRequired: Feld muss beim de-/serialisieren einen Wert haben
		//JsonExtensionData: Fängt Felder, welche beim deserialisieren kein entsprechendes Feld in der C# Klasse haben
		//JsonConstructor: Gibt an, welcher Konstruktor beim deserialisieren verwendet werden soll

		//4. Json per Hand durchgehen
		JToken doc = JToken.Parse(readJson);
		foreach (JToken element in doc)
		{
			Console.WriteLine(element["MaxV"].Value<int>());
			Console.WriteLine((FahrzeugMarke) element["Marke"].Value<int>());
			Console.WriteLine("------------------------");

			//Sobald ein BMW gefunden wurde, stoppen
			FahrzeugMarke current = (FahrzeugMarke) element["Marke"].Value<int>();
			if (current == FahrzeugMarke.BMW)
				return;
		}
	}
}

//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]
//[JsonDerivedType(typeof(PKWRot), "PR")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
[XmlInclude(typeof(PKWRot))]

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	[JsonRequired]
	//[XmlAttribute]
	public int MaxV { get; set; }

	//[JsonIgnore]
	//[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public class PKWRot : PKW
{
	public PKWRot(int maxV, FahrzeugMarke marke) : base(maxV, marke)
	{
	}

    public PKWRot()
    {
		
    }
}