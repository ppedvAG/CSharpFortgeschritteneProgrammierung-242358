using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Zur Laufzeit indirekt mit Objekten interagieren

		//Alles bei Reflection geht von Type Objekten aus

		//GetType()
		Program p = new Program();
		Type t1 = p.GetType(); //Typen via Objekt

		//typeof(...)
		Type t2 = typeof(Program); //Typen über einen Typnamen (Klasse, Interface, Delegate, ...)

		//Alle möglichen Eigenschaften von Objekten herausfinden:
		MethodInfo[] methods = t1.GetMethods();
		PropertyInfo[] properties = t1.GetProperties();
		ConstructorInfo[] constructors = t1.GetConstructors();
		FieldInfo[] fields = t1.GetFields();

		//Aufgabe: Methode von einem Objekt des Typs Program ausführen
		t1.GetMethod("Test").Invoke(p, null);
		t1.GetProperty("Text").SetValue(p, "Hallo Property");
        Console.WriteLine(t1.GetProperty("Text").GetValue(p));

		/////////////////////////////////////////////////////////////////////////

		//Aufgabe: Event Komponente per Reflection erstellen, Events anhängen, Run() ausführen

		//Zwei weitere Schritte: DLL laden, Objekt indirekt erstellen
		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_12_09\Delegates\bin\Debug\net8.0\Delegates.dll");
		//a enthält alle Typen des Projekts
		Type ec = a.GetType("Delegates.EventComponent");

		//Activator: Objekte über einen Typen (Type Objekt) erstellen
		object comp = Activator.CreateInstance(ec);
		ec.GetEvent("Start").AddEventHandler(comp, new EventHandler((object sender, EventArgs args) => Console.WriteLine("Reflection Start")));
		ec.GetEvent("End").AddEventHandler(comp, new EventHandler((object sender, EventArgs args) => Console.WriteLine("Reflection Stop")));
		ec.GetEvent("Progress").AddEventHandler(comp, new EventHandler<int>((object sender, int x) => Console.WriteLine($"Reflection Fortschritt: {x}")));
		ec.GetMethod("Run").Invoke(comp, null);
    }

	public string Text { get; set; }

	public void Test()
	{
        Console.WriteLine("Hallo Methode");
    }
}
