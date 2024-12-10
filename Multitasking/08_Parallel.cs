﻿using System.Diagnostics;

namespace Multitasking;

public class _08_Parallel
{
	static void Main(string[] args)
	{
		int[] durchgänge = [1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000];
		foreach (int d in durchgänge)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Durchgänge: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}, {sw2.ElapsedMilliseconds}ms");

			Console.WriteLine("------------------------------------------------------------");
		}

		/*
			For Durchgänge: 1000, 0ms
			ParallelFor Durchgänge: 1000, 41ms
			------------------------------------------------------------
			For Durchgänge: 10000, 1ms
			ParallelFor Durchgänge: 10000, 2ms
			------------------------------------------------------------
			For Durchgänge: 50000, 12ms
			ParallelFor Durchgänge: 50000, 10ms
			------------------------------------------------------------
			For Durchgänge: 100000, 18ms
			ParallelFor Durchgänge: 100000, 10ms
			------------------------------------------------------------
			For Durchgänge: 250000, 38ms
			ParallelFor Durchgänge: 250000, 11ms
			------------------------------------------------------------
			For Durchgänge: 500000, 63ms
			ParallelFor Durchgänge: 500000, 36ms
			------------------------------------------------------------
			For Durchgänge: 1000000, 102ms
			ParallelFor Durchgänge: 1000000, 32ms
			------------------------------------------------------------
			For Durchgänge: 5000000, 475ms
			ParallelFor Durchgänge: 5000000, 222ms
			------------------------------------------------------------
			For Durchgänge: 10000000, 990ms
			ParallelFor Durchgänge: 10000000, 329ms
			------------------------------------------------------------
			For Durchgänge: 100000000, 10211ms
			ParallelFor Durchgänge: 100000000, 2917ms
			------------------------------------------------------------
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i =>
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}