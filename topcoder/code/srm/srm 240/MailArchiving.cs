using System;

	class MailArchiving
	{
		public int minSelections(string[] destFolders)
		{
			int n = destFolders.Length;
			int[,] dp = new int[n, n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
				{
					dp[i, j] = int.MaxValue;
				}
				dp[i, i] = 1;
			}
			for (int len = 2; len <= n; ++len)
				for (int i = 0; i <= n - len; ++i)
				{
					int j = i + len - 1;
					dp[i, j] = dp[i + 1, j] + 1;
					for (int k = i + 1; k <= j; ++k)
					{
						if (destFolders[i].Equals(destFolders[k]))
						{
							if (k - 1 >= i + 1)
							{
								dp[i, j] = Math.Min(dp[i, j], dp[i + 1, k - 1] + dp[k, j]);
							}
							else
							{
								dp[i, j] = Math.Min(dp[i, j], dp[k, j]);
							}
						}
					}
				}
			return dp[0, n - 1];
		}
		static void Main(string[] args)
		{
			MailArchiving ma = new MailArchiving();
			Console.WriteLine(ma.minSelections(new string[] {"Deleted messages","Saved messages","Deleted messages"}));
			Console.WriteLine(ma.minSelections(new string[] {"Folder A","Folder B","Folder C","Folder D","Folder E","Folder F"}));
			Console.WriteLine(ma.minSelections(new string[] {"FOLDER","folder"}));
			Console.WriteLine(ma.minSelections(new string[] {"a","b","a","c","a","a","b","a","c","d","a"}));
			Console.WriteLine(ma.minSelections(new string[] {"a","b","b","c","d","e","d","e","d","e",
																"c","c","a","a","a","f","g","g","f","a",
																"h","h","i","j","i","j","a","a","k","k",
																"l","m","k","l","m","a","o","o","p","a"}));

			Console.ReadLine();
		}
	}

