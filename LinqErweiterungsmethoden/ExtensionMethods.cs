using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		//int summe = 0;
		//string zahlAlsString = x.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static string AsString<T>(this IEnumerable<T> values)
	{
		StringBuilder sb = new("[");
        foreach (T item in values)
        {
            sb.Append(item.ToString());
            sb.Append(", ");
        }
		return sb.ToString().TrimEnd(',', ' ') + "]";
    }

	public static JsonElement GetProperty(this JsonElement element, params string[] propertyName)
	{
		foreach (string str in propertyName)
		{
			element = element.GetProperty(str);
		}
		return element;
	}
}