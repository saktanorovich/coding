import java.io.*;
import java.util.*;

public class Main {
    private static class Country {
        public String name;
        public int euro;
        public int asof;
        public int size;
        public int xl, yl;
        public int xh, yh;

        public Country(String name, int euro, int xl, int yl, int xh, int yh) {
            this.name = name;
            this.euro = euro;
            this.asof = 0;
            this.size = 0;
            this.xl = xl;
            this.yl = yl;
            this.xh = xh;
            this.yh = yh;
        }
    }

    private static boolean scan(int day, Country[] countries, int[][] marker, int[][] subset, int[][][] budget) {
        int numOfCoins = countries.length;
        for (Country country : countries) {
            int numOfCities = 0;
            for (int x = country.xl; x <= country.xh; ++x) {
                for (int y = country.yl; y <= country.yh; ++y) {
                    if (Integer.bitCount(subset[x][y]) == numOfCoins) {
                        ++numOfCities;
                    }
                }
            }
            if (country.size == numOfCities) {
                if (country.asof > day) {
                    country.asof = day;
                }
            }
        }
        int numOfComplete = 0;
        for (Country country : countries) {
            if (country.asof < Integer.MAX_VALUE) {
                ++numOfComplete;
            }
        }
        return numOfComplete == countries.length;
    }

    private static void wave(int day, Country[] countries, int[][] marker, int[][] subset, int[][][] budget) {
        int dx[] = new int[] { -1, 0, +1, 0 };
        int dy[] = new int[] { 0, -1, 0, +1 };
        int[][][] incr = new int[20][20][20];
        for (Country country : countries) {
            for (int x = country.xl; x <= country.xh; ++x) {
                for (int y = country.yl; y <= country.yh; ++y) {
                    for (int b = 0; b < countries.length; ++b) {
                        int portion = budget[x][y][b] / 1000;
                        if (portion > 0) {
                            for (int k = 0; k < 4; ++k) {
                                int nx = x + dx[k];
                                int ny = y + dy[k];
                                if (marker[nx][ny] != 0) {
                                    budget[x][y][b] -= portion;
                                    incr[nx][ny][b] += portion;
                                }
                            }
                        }
                    }
                }
            }
        }
        for (Country country : countries) {
            for (int x = country.xl; x <= country.xh; ++x) {
                for (int y = country.yl; y <= country.yh; ++y) {
                    for (int b = 0; b < countries.length; ++b) {
                        budget[x][y][b] += incr[x][y][b];
                        if (budget[x][y][b] > 0) {
                            subset[x][y] = subset[x][y] | (1 << b);
                        }
                    }
                }
            }
        }
    }

    private static void doit(Country[] countries) {
        int[][] marker = new int[20][20];
        int[][] subset = new int[20][20];
        int[][][] budget = new int[20][20][countries.length];
        for (Country country : countries) {
            country.asof = Integer.MAX_VALUE;
            int euro = country.euro;
            for (int x = country.xl; x <= country.xh; ++x) {
                for (int y = country.yl; y <= country.yh; ++y) {
                    marker[x][y] = euro + 1;
                    subset[x][y] = 1 << euro;
                    budget[x][y][euro] = (int)1e+6;
                    ++country.size;
                }
            }
        }
        for (int day = 0; true; ++day) {
            if (scan(day, countries, marker, subset, budget)) {
                break;
            }
            wave(day, countries, marker, subset, budget);
        }
        Arrays.sort(countries, (a, b) -> {
            if (a.asof != b.asof) {
                return a.asof - b.asof;
            }
            return a.name.compareTo(b.name);
        });
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        PrintWriter out = new PrintWriter(System.out);
        for (int testCase = 1; true; ++testCase) {
            int numOfCountries = in.nextInt();
            if (numOfCountries == 0) {
                break;
            }
            Country[] countries = new Country[numOfCountries];
            for (int euro = 0; euro < numOfCountries; ++euro) {
                String name = in.next();
                int xl = in.nextInt();
                int yl = in.nextInt();
                int xh = in.nextInt();
                int yh = in.nextInt();
                countries[euro] = new Country(name, euro, xl, yl, xh, yh);
            }
            if (numOfCountries > 1) {
                doit(countries);
            }
            out.printf("Case Number %d\n", testCase);
            for (Country country : countries) {
                out.printf("   %s   %d\n", country.name, country.asof);
            }
        }
        in.close();
        out.close();
    }
}
