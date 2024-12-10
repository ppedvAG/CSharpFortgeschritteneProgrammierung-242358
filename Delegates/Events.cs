using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Delegates;

public class Events
{
	/// <summary>
	/// Event: Schnittstelle für andere Entwickler
	/// 
	/// Besteht immer aus einem beliebigen Delegate (meistens EventHandler) + dem Event Keyword
	/// 
	/// Idee: Schnittstelle bereitstellen, damit andere Entwickler eigenen Code in unseren einhängen können
	/// Beispiele: ObservableCollection, WPF
	/// 
	/// Drei Teile:
	/// - Event selbst
	/// - Anhängen einer Methode
	/// - Ausführen des Events
	/// 
	/// Zwei Seiten:
	/// - Entwickler: Legt das Event an und führt es aus
	/// - Anwender: Hängt seinen eigenen Code an
	/// </summary>
	static void Main(string[] args) => new Events().Run();

	public event EventHandler TestEvent; //Entwicklerseite

	public event EventHandler<int> IntEvent;

	public event Action<int> IntActionEvent; //Auch Action hier möglich

	public void Run()
	{
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		//////////////////////////////////////////////

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(this, 10);

		//////////////////////////////////////////////

		IntActionEvent += Events_IntActionEvent;
		IntActionEvent?.Invoke(10);

		//////////////////////////////////////////////
		
		ObservableCollection<int> ints = new ObservableCollection<int>();
		ints.CollectionChanged += Ints_CollectionChanged;
		ints.Add(1);
		ints.Add(2);
		ints.Add(3);
		ints.Remove(2);
	}

	private void Ints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
                Console.WriteLine($"Neues Element hinzugefügt: {e.NewItems[0]}");
                break;
			case NotifyCollectionChangedAction.Remove:
				Console.WriteLine($"Element entfernt: {e.OldItems[0]}");
				break;
		}
	}

	//Anwenderseite
	//Wenn von dem Event Invoke ausgeführt wird, wird der Code innerhalb dieser Methode ausgeführt
	private void Events_TestEvent(object sender, EventArgs e)
	{
        Console.WriteLine("Hallo Events");
	}

	private void Events_IntEvent(object sender, int e)
	{
        Console.WriteLine($"Die Zahl: {e}");
	}

	private void Events_IntActionEvent(int obj)
	{
        Console.WriteLine(obj);
    }
}