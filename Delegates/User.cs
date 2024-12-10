namespace Delegates;

public class User
{
	/// <summary>
	/// Anwenderseite: Hängt die Events an und definiert den Code, welcher bei feuern der Events ausgeführt wird
	/// </summary>
	static void Main(string[] args)
	{
		EventComponent comp = new();
		comp.Start += Comp_Start;
		comp.Progress += Comp_Progress;
		comp.End += Comp_End;
		comp.Run();
	}

	private static void Comp_End(object sender, EventArgs e)
	{
		Console.WriteLine($"Prozess beendet");
	}

	private static void Comp_Progress(object sender, int e)
	{
		Console.WriteLine($"Fortschritt: {e}");
	}

	private static void Comp_Start(object sender, EventArgs e)
	{
        Console.WriteLine($"Prozess gestartet");
    }
}