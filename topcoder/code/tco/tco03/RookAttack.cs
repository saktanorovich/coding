using System;
using System.Collections.Generic;

class RookAttack
{
    private const int oo = 1000000000;

    class HopcroftKarp
    {
        struct Pair<F, S>
        {
            public readonly F First;
            public readonly S Second;

            public Pair(F first, S second)
            {
                First = first;
                Second = second;
            }
        }

        private int n1, n2;
        private int[] matched;
        private int[] layer;
        private bool[] used;
        private List<int> endings;
        private bool[,] g;

        private bool augment()
        {
            for (int i = 0; i < n1 + n2; ++i)
            {
                layer[i] = -oo;
            }
            Queue<int> q = new Queue<int>();
            Queue<int> qh = new Queue<int>();
            for (int i = 0; i < n1; ++i)
            {
                if (matched[i] == -oo)
                {
                    layer[i] = 0;
                    q.Enqueue(i);
                }
            }
            endings.Clear();
            while (q.Count > 0)
            {
                while (q.Count > 0)
                {
                    int u = q.Dequeue();
                    for (int v = 0; v < n1 + n2; ++v)
                    {
                        if (g[u, v])
                        {
                            if (layer[v] == -oo)
                            {
                                layer[v] = layer[u] + 1;
                                if (matched[v] == -oo)
                                {
                                    endings.Add(v);
                                }
                                else
                                {
                                    qh.Enqueue(v);
                                }
                            }
                        }
                    }
                }
                if (endings.Count > 0)
                {
                    return true;
                }
                while (qh.Count > 0)
                {
                    int u = qh.Dequeue();
                    int v = matched[u];
                    layer[v] = layer[u] + 1;
                    q.Enqueue(v);
                }
            }
            return false;
        }

        private List<Pair<int, int>> getAugmentingPath(int u)
        {
            if (!used[u])
            {
                used[u] = true;
                for (int v = 0; v < n1 + n2; ++v)
                {
                    if (g[u, v])
                    {
                        if (layer[u] == layer[v] + 1 && !used[v])
                        {
                            if (layer[v] == 0)
                            {
                                used[v] = true;
                                List<Pair<int, int>> result = new List<Pair<int, int>>();
                                result.Add(new Pair<int, int>(v, u));
                                return result;
                            }
                            {
                                List<Pair<int, int>> result = getAugmentingPath(matched[v]);
                                if (result.Count > 0)
                                {
                                    result.Insert(0, new Pair<int, int>(v, u));
                                    return result;
                                }
                            }
                        }
                    }
                }
            }
            return new List<Pair<int, int>>();
        }

        private int maximumMatching()
        {
            for (int i = 0; i < n1 + n2; ++i)
            {
                matched[i] = -oo;
            }
            while (augment())
            {
                used = new bool[n1 + n2];
                for (int i = 0; i < endings.Count; ++i)
                {
                    int ending = endings[i];
                    List<Pair<int, int>> p = getAugmentingPath(ending);
                    if (p.Count > 0)
                    {
                        for (int j = 0; j < p.Count; ++j)
                        {
                            int u = p[j].First;
                            int v = p[j].Second;
                            matched[u] = v;
                            matched[v] = u;
                        }
                    }
                }
            }
            int result = 0;
            for (int i = 0; i < n1; ++i)
            {
                if (matched[i] != -oo)
                {
                    ++result;
                }
            }
            return result;

        }

        public int matching(int n1, int n2, bool[,] graph)
        {
            this.n1 = n1;
            this.n2 = n2;
            matched = new int[n1 + n2];
            layer = new int[n1 + n2];
            endings = new List<int>();
            g = new bool[n1 + n2, n1 + n2];
            for (int i = 0; i < n1; ++i)
            {
                for (int j = 0; j < n2; ++j)
                {
                    if (graph[i, j])
                    {
                        g[i, j + n1] = true;
                        g[j + n1, i] = true;
                    }
                }
            }
            return maximumMatching();
        }
    }

    struct Segment
    {
	    public int id;
	    public int lo, hi;

	    public Segment(int id, int lo, int hi)
	    {
		    this.id = id;
		    this.lo = lo;
		    this.hi = hi;
	    }
    }

    private int nrows, ncolumns;
    private bool[,] field;
    private bool[,] g;
    private List<Segment> rowSegments;
    private List<Segment> columnSegments;

    void buildSegments()
    {
        rowSegments = new List<Segment>();
        for (int r = 0; r < nrows; ++r)
        {
            rowSegments.Add(new Segment(r, 0, ncolumns - 1));
        }
        columnSegments = new List<Segment>();
        for (int c = 0; c < ncolumns; ++c)
        {
            columnSegments.Add(new Segment(c, 0, nrows - 1));
        }
    }

    void buildGraph()
    {
        g = new bool[rowSegments.Count, columnSegments.Count];
        for (int i = 0; i < rowSegments.Count; ++i)
        {
            Segment rsegment = rowSegments[i];
            for (int j = 0; j < columnSegments.Count; ++j)
            {
                Segment csegment = columnSegments[j];
                int r = rsegment.id;
                int c = csegment.id;
                if (rsegment.lo <= c && c <= rsegment.hi &&
                    csegment.lo <= r && r <= csegment.hi)
                {
                    if (!field[r, c])
                    {
                        g[i, j] = true;
                    }
                }
            }
        }
    }

    public int howMany(int rows, int cols, string[] cutouts)
    {
        nrows = rows;
        ncolumns = cols;
        field = new bool[nrows + 1, ncolumns + 1];
        for (int i = 0; i < cutouts.Length; ++i)
        {
            string[] items = cutouts[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                int[] c =
                    Array.ConvertAll<string, int>(
                        item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                        delegate(string s) { return int.Parse(s); });
                field[c[0], c[1]] = true;
            }
        }
        for (int i = 0; i < nrows; ++i)
        {
            field[i, ncolumns] = true;
        }
        for (int j = 0; j < ncolumns; ++j)
        {
            field[nrows, j] = true;
        }

	    buildSegments();
	    buildGraph();
        HopcroftKarp hk = new HopcroftKarp();
        int result = hk.matching(rowSegments.Count, columnSegments.Count, g);
        return result;
    }

    static void Main(string[] args)
    {
        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(8, 8, new string[] { }));
        }

        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(2, 2, new string[] { "0 0", "0 1", "1 1", "1 0" }));
        }

        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(3, 3,
                                         new string[] { "0 0", "1 0", "1 1", "2 0", "2 1", "2 2" }));
        }

        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(3, 3, new string[] { "0 0", "1 2", "2 2" }));
        }

        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(200, 200, new string[] { }));
        }

        {
            RookAttack ra = new RookAttack();
            Console.WriteLine(ra.howMany(4, 4, new string[] { "0 0", "0 1", "1 2", "2 0", "2 1", "2 0", "2 1" }));
        }

        Console.ReadLine();
    }
}
