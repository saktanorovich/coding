using System;

public class StickedWords
{
    private const int alphabetSize = 26;

    public string constructLine(string[] dict, int len)
    {
        int n = dict.Length;

        string[,] best = new string[256, alphabetSize];
        string res = "";

        for (int i = 0; i < 256; ++i)
            for (int c = 0; c < alphabetSize; ++c)
            {
                best[i, c] = "";
            }

        string[][] a = new string[alphabetSize][];
        int[] cnt = new int[alphabetSize];

        for (int i = 0; i < n; ++i)
        {
            int c = (int)dict[i][0] - (int)'a';
            cnt[c]++;
            best[1, c] = string.Concat(dict[i][0]);
        }
        for (int c = 0; c < alphabetSize; ++c)
        {
            a[c] = new string[cnt[c]];
            cnt[c] = 0;
        }
        for (int i = 0; i < n; ++i)
        {
            int c = (int)dict[i][0] - (int)'a';
            a[c][cnt[c]++] = dict[i].Remove(0, 1);
        }

        for (int i = 1; i <= len; ++i)
            for (int c = 0; c < alphabetSize; ++c)
            {
                int k = i % 256;
                if (best[k, c] != "")
                {
                    for (int j = 0; j < cnt[c]; ++j)
                    {
                        string str = best[k, c] + a[c][j];
                        int strLen = i + a[c][j].Length;
                        int last = (int)str[strLen - 1] - (int)'a';
                        if (strLen < len)
                        {
                            if (best[strLen % 256, last] == "" || str.CompareTo(best[strLen % 256, last]) < 0)
                            {
                                best[strLen % 256, last] = str;
                            }
                        }
                        else if (res == "" || str.CompareTo(res) < 0)
                        {
                            res = str;
                        }
                    }
                    best[k, c] = "";
                }
            }
        return res;
    }
}
