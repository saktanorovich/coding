using System;
using System.Collections.Generic;

class Parking
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

    private readonly int[] dx = new int[] { 0, -1, 0, +1 };
    private readonly int[] dy = new int[] { -1, 0, +1, 0 };

    private int n, m;
    private string[] park;
    private int[] d;
    private int[,] time;
    private bool[,] g;
    private List<int> cars;
    private List<int> parkingSpots;
    private int[,] carByPosition;
    private int[,] parkingSpotByPosition;

    private int code(int x, int y)
    {
        return x * m + y;
    }

    private void bfs(int car)
    {
        int carPosition = cars[car];
        for (int i = 0; i < n * m; ++i)
        {
            d[i] = +oo;
        }
        d[carPosition] = 0;
        Queue<int> q = new Queue<int>();
        q.Enqueue(carPosition);
        while (q.Count > 0)
        {
            int p = q.Dequeue();
            int px = p / m;
            int py = p % m;
            for (int i = 0; i < 4; ++i)
            {
                int x = px + dx[i];
                int y = py + dy[i];
                if (0 <= x && x < n && 0 <= y && y < m)
                {
                    int spot = code(x, y);
                    if (park[x][y] != 'X')
                    {
                        if (d[spot] > d[p] + 1)
                        {
                            d[spot] = d[p] + 1;
                            q.Enqueue(spot);
                            if (park[x][y] == 'P')
                            {
                                int parkingSpot = parkingSpotByPosition[x, y];
                                time[car, parkingSpot] = d[spot];
                            }
                        }
                    }
                }
            }
        }
    }

    private bool target(int timeLimit)
    {
        for (int i = 0; i < cars.Count; ++i)
        {
            for (int j = 0; j < parkingSpots.Count; ++j)
            {
                g[i, j] = false;
                if (1 <= time[i, j] && time[i, j] <= timeLimit)
                {
                    g[i, j] = true;
                }
            }
        }
        HopcroftKarp hk = new HopcroftKarp();
        return hk.matching(cars.Count, parkingSpots.Count, g) == cars.Count;
    }

    private void initialize()
    {
        d = new int[n * m];
        cars = new List<int>();
        parkingSpots = new List<int>();
        carByPosition = new int[n, m];
        parkingSpotByPosition = new int[n, m];
        park = new string[n];
    }

    public int minTime(string[] parking)
    {
        n = parking.Length;
        m = parking[0].Length;
        initialize();
        for (int i = 0; i < n; ++i)
        {
            park[i] = parking[i];
            for (int j = 0; j < m; ++j)
            {
                carByPosition[i, j] = -oo;
                parkingSpotByPosition[i, j] = -oo;
                if (park[i][j] == 'C')
                {
                    cars.Add(code(i, j));
                    carByPosition[i, j] = cars.Count - 1;
                }
                else if (park[i][j] == 'P')
                {
                    parkingSpots.Add(code(i, j));
                    parkingSpotByPosition[i, j] = parkingSpots.Count - 1;
                }
            }
        }
        g = new bool[cars.Count, parkingSpots.Count];
        time = new int[cars.Count, parkingSpots.Count];
        for (int i = 0; i < cars.Count; ++i)
        {
            bfs(i);
        }
        int lo = 0, hi = 0;
        for (int i = 0; i < cars.Count; ++i)
        {
            for (int j = 0; j < parkingSpots.Count; ++j)
            {
                hi = Math.Max(hi, time[i, j]);
            }
        }
        while (lo + 1 < hi)
        {
            int timeLimit = (lo + hi) / 2;
            if (target(timeLimit))
            {
                hi = timeLimit;
            }
            else
            {
                lo = timeLimit + 1;
            }
        }
        for (int i = lo; i <= hi; ++i)
        {
            if (target(i))
            {
                return i;
            }
        }
        return -1;
    }

    static void Main(string[] args)
    {
        {
            Parking p = new Parking();
            Console.WriteLine(p.minTime(new string[] { "C.....P", "C.....P", "C.....P" }));
        }

        {
            Parking p = new Parking();
            Console.WriteLine(
                p.minTime(new string[] { "C.X.....", "..X..X..", "..X..X..", ".....X.P" }));
        }


        {
            Parking p = new Parking();
            Console.WriteLine(
                p.minTime(new string[]
                {
                    "XXXXXXXXXXX", "X......XPPX", "XC...P.XPPX", "X......X..X", "X....C....X",
                    "XXXXXXXXXXX"
                }));
        }


        {
            Parking p = new Parking();
            Console.WriteLine(p.minTime(new string[] { ".C.", "...", "C.C", "X.X", "PPP" }));
        }



        {
            Parking p = new Parking();
            Console.WriteLine(p.minTime(new string[] { "CCCCC", ".....", "PXPXP" }));
        }


        {
            Parking p = new Parking();
            Console.WriteLine(p.minTime(new string[] { "..X..", "C.X.P", "..X.." }));
        }

        Console.ReadLine();
    }
}
