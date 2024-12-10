namespace Delegates;

/// <summary>
/// Simuliert einen länger andauernden Prozess (z.B. DB, Webschnittstelle, ...)
/// 
/// Hier sollen drei Events definiert werden, welche den User über den Fortschritt informieren sollen
/// </summary>
public class EventComponent
{
	public event EventHandler Start;

	public event EventHandler End;

	public event EventHandler<int> Progress;

	/// <summary>
	/// Entwicklerseite: Definiert die Events und führt diese aus
	/// </summary>
	public void Run()
	{
		Start?.Invoke(this, EventArgs.Empty);

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(this, i);
		}

		End?.Invoke(this, EventArgs.Empty);
	}
}