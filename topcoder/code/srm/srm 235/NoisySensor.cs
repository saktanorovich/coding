using System;

class NoisySensor
{
	private int median(int a, int b, int c)
	{
		int[] ar = new int[3];
		ar[0] = a;
		ar[1] = b;
		ar[2] = c;
		Array.Sort(ar);
		return ar[1];
	}
	public int[] medianFilter(int[] data)
	{
		int n = data.Length;
		int[] result = new int[n];
		result[0] = data[0];
		result[n - 1] = data[n - 1];
		for (int i = 1; i < n - 1; ++ i)
		{
			result[i] = median(data[i - 1], data[i], data[i + 1]);
		}
		return result;
	}
	static void Main(string[] args)
	{
		NoisySensor ns = new NoisySensor();

		Console.WriteLine(ns.medianFilter(new int[]{10, 1, 9, 2, 8}));

		Console.ReadLine();

	}
}
