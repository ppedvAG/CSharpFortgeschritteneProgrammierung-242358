using System.Buffers;
using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();

		//Sequentiell
		//Toast();
		//Tasse();
		//Kaffee();

		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //7s

		/////////////////////////////////////////////

		//Mit WaitAll
		//Problem: Keine richtige Abfolge

		//Task t1 = new Task(Toast);
		//Task t2 = new Task(Tasse);
		//Task t3 = new Task(Kaffee);
		//t1.Start();
		//t2.Start();
		//t3.Start();
		//Task.WaitAll(t1, t2, t3);

		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //4s

		/////////////////////////////////////////////

		//Mit ContinueWith
		//Task t1 = new Task(Toast); //Toast in den Toaster werfen
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(t => Kaffee());
		//t1.Start();
		//t2.Start();

		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //0s
		//Console.ReadKey(); //Problem: Tasks sind im Hintergrund -> WaitAll

		/////////////////////////////////////////////

		//Das Beispiel funktional
		//Task t1 = new Task(Toast);
		//Task t2 = new Task(Tasse);
		//Task t3 = t2.ContinueWith(x => Kaffee()).ContinueWith(TaskEnd);
		//t1.ContinueWith(TaskEnd);
		//t1.Start();
		//t2.Start();

		//void TaskEnd(Task t)
		//{
		//	if (t.IsCompletedSuccessfully)
		//	{
		//		stopwatch.Stop();
		//		Console.WriteLine(stopwatch.ElapsedMilliseconds);
		//	}
		//}

		//Console.ReadKey();

		/////////////////////////////////////////////

		//Async/Await

		//async
		//async wird immer auf eine Methode angewandt
		//async hängt immer mit dem RÜckgabewert zusammen
		//Eine async-Methode muss einen von drei Rückgabetypen haben:
		//- async void: Kann await benutzen (in der Methode), kann selbst aber nicht awaited werden
		//- async Task: Kann await benutzen (in der Methode), kann selbst awaited werden
		//- async Task<T>/ValueTask<T>/IAsyncEnumerable: Selbiges wie async Task, muss aber ein Ergebnis zurückgeben

		//await
		//Äquivalent zu t.Wait und t.Result
		//Blockiert nicht (generiert im Hintergrund Code, welcher dafür verantwortlich ist)
		//Wenn eine Methode mit async Task<T>/ValueTask<T>/IAsyncEnumerable awaited wird, kommt hier das Ergebnis zurück
		//int x = t.Result; -> int x = await t;

		//Task t1 = ToastAsync(); //Wenn eine async Task/Task<T> Methode gestartet wird, wird diese als Task gestartet
		//Task t2 = TasseAsync(); //Starte Tasse
		//await t2; //Warte auf die Tasse
		//Task t3 = KaffeeAsync(); //Starte Kaffee
		//await t3; //Warte auf Kaffee
		//await t1; //Warte auf Toast

		//Console.WriteLine(stopwatch.ElapsedMilliseconds);

		/////////////////////////////////////////////

		//Erweiterung: Objekte als Rückgabewerte mit abschließendem Frühstücksobjekt
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = TasseObjectAsync();
		//Tasse tasse = await t2; //await macht auch ein t2.Result
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse);
		//Kaffee kaffee = await t3;
		//Toast toast = await t1;

		//Frühstück f = new Frühstück(toast, kaffee);
		//Console.WriteLine(stopwatch.ElapsedMilliseconds);

		//Vereinfachung
		Task<Toast> t1 = ToastObjectAsync();
		Kaffee kaffee = await KaffeeObjectAsync(await TasseObjectAsync());
		Frühstück f = new Frühstück(await t1, kaffee);

		Console.WriteLine(stopwatch.ElapsedMilliseconds);

		/////////////////////////////////////////////
		
		//Aufbau:
		//- Start der Aufgabe (Task t = ...)
		//- Zwischenschritte
		//- Auf Aufgabe warten mit await (await t)
	}

	#region Normal
	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region async
	static async Task ToastAsync()
	{
		await Task.Delay(4000);
        Console.WriteLine("Toast fertig");
		//Kein Return
    }

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		//Kein Return
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		//Kein Return
	}
	#endregion

	#region async mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public record Toast();

public record Tasse();

public record Kaffee(Tasse t);

public record Frühstück(Toast t, Kaffee k);