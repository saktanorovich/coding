using System;

	class PowerCollector
	{
		private ulong pow(ulong x, int p)
		{
			if (p == 1)
			{
				return x;
			}
			if (p % 2 == 0)
				return pow(x * x, p / 2);
			else
				return x * pow(x, p - 1);
		}
		ulong root(ulong x, int p, ulong rprev)
		{
			ulong r = Math.Max((ulong)Math.Exp(Math.Log(x) / p) - 1, 0);
			while (pow(r, p) <= x && r <= rprev)
			{
				++r;
			}
			return r - 1;
		}
		public string countPowers(string sn)
		{
			ulong n = ulong.Parse(sn);
			ulong rprev = n;
			ulong result = root(n, 2, rprev);
			for (int p = 3; ; ++p)
			{
				ulong r = root(n, p, rprev);
				rprev = r;
				if (r < 2)
				{
					break;
				}
				for (int i = 2; i <= (int)r; ++i)
				{
					bool ok = true;
					ulong what = pow((ulong)i, p);
					for (int j = 2; ok && j < p; ++j)
					{
						if (pow(root(what, j, n), j) == what)
						{
							ok = false;
						}
					}
					if (ok)
					{
						++result;
					}
				}
			}
			return result.ToString();
		}
		static void Main(string[] args)
		{
			PowerCollector pc = new PowerCollector();
			Console.WriteLine(pc.countPowers("1") == "1");
 			Console.WriteLine(pc.countPowers("10") == "4");
 			Console.WriteLine(pc.countPowers("100") == "13");
 			Console.WriteLine(pc.countPowers("1000") == "41");
 			Console.WriteLine(pc.countPowers("10000") == "125");
			Console.WriteLine(pc.countPowers("576460752303423487") == "760085355");
			Console.WriteLine(pc.countPowers("999999999999999999") == "1001003331");
			Console.WriteLine(pc.countPowers("1000000000000000000"));

			Console.ReadLine();
		}
	}

