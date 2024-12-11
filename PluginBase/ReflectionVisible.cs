namespace PluginBase;

/// <summary>
/// Attribute selbst haben keine Effekte
/// Durch Reflection können diese ausgewertet werden
/// </summary>
public class ReflectionVisible : Attribute
{
	public string Name { get; private set; }

    public ReflectionVisible()
    {
        
    }

    public ReflectionVisible(string name)
    {
		Name = name;
    }
}