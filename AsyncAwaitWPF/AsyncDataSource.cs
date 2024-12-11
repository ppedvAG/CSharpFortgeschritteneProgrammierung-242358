namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	/// <summary>
	/// Beispiel: Livestream
	/// 
	/// Wirft kontinuierlich Daten zurück an die Clients
	/// Die Clients können diese Daten verarbeiten
	/// Wenn ein Client erscheint, bekommt er keine Daten aus der Vergangenheit
	/// Wenn ein Client verschwindet, bekommt er keine neuen Daten gesendet
	/// </summary>
	public async IAsyncEnumerable<int> GetNumbers()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 500));
			yield return Random.Shared.Next();
		}
	}
}