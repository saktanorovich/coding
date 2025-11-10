using System;

	class CantorSet
	{
		public int removed(string num)
		{
			int[] a = new int[num.Length - 1];
			for (int i = 1; i < num.Length; ++i)
			{
				a[i - 1] = int.Parse(num[i].ToString());
			}
			for (int i = 1; i <= 1000000; ++i)
			{
				int carry = 0;
				for (int j = a.Length - 1; j >= 0; --j)
				{
					a[j] = a[j] * 3 + carry;
					carry = a[j] / 10;
					a[j] %= 10;
				}
				if (carry == 1)
				{
					return i;
				}
			}
			return 0;
		}
		static void Main(string[] args)
		{
			CantorSet cs = new CantorSet();
			Console.WriteLine(cs.removed(".200"));
			Console.WriteLine(cs.removed(".74928"));
			Console.WriteLine(cs.removed(".975"));
			Console.WriteLine(cs.removed(".123460963214612348612934912348602164312630416043"));

			Console.ReadLine();
		}
	}

