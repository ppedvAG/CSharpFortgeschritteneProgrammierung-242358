﻿namespace Generic;

public class Constraints
{
	public void TestVererbung()
	{
		IEnumerable<Lebewesen> lw = new List<Mensch>();
		//lw.Add(new Mensch());
	}

	public T Test<T>()// where T : new()
		where T : class
	{
		//return new T(); //Was ist, wenn T keinen Standardkonstruktor hat?

		T t = null;
		return t;
	}
}

public class DataStore1<T> where T : struct; //T muss ein Wertetyp sein

public class DataStore2<T> where T : class; //T muss ein Wertetyp sein

public class DataStore3<T> where T : new(); //T muss einen Standardkonstruktor haben

public class DataStore4<T> where T : Enum; //T muss ein Enumtyp sein

public class DataStore5<T> where T : Delegate; //T muss ein Delegatetyp sein

public class DataStore6<T> where T : unmanaged; //T muss ein Basisdatentyp oder ein Pointertyp sein

public class DataStore7<T> where T : notnull; //T darf nicht Nullable sein

public class DataStore8<T1, T2>
	where T1 : class, new() //Mehrere Constrains mit Komma getrennt
	where T2 : struct; //Mehrere Constraints auf mehrere Generics


public class Lebewesen;

public class Mensch : Lebewesen;