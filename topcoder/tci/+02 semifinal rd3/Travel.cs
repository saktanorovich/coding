using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Travel {
            public int shortest(string[] city, int radius) {
                  cities = new City[city.Length];
                  for (int i = 0; i < city.Length; ++i) {
                        string[] items = city[i].Split(new char[] { ' ' });
                        cities[i] = new City(int.Parse(items[0]), int.Parse(items[1]));
                  }
                  memo = new double[1 << city.Length, city.Length];
                  for (int set = 0; set < 1 << city.Length; ++set) {
                        for (int last = 0; last < city.Length; ++last) {
                              memo[set, last] = -1;
                        }
                  }
                  memo[1, 0] = 0;
                  double result = double.MaxValue;
                  for (int last = 1; last < city.Length; ++last) {
                        result = Math.Min(result, shortest((1 << city.Length) - 1, last) + cities[last].Distance(cities[0]));
                  }
                  return (int)(radius * result + 0.5);
            }

            private double[,] memo;
            private City[] cities;

            private double shortest(int set, int last) {
                  if (memo[set, last] == -1) {
                        memo[set, last] = double.MaxValue;
                        int next = set ^ (1 << last);
                        for (int city = 0; city < cities.Length; ++city) {
                              if ((next & (1 << city)) != 0) {
                                    memo[set, last] = Math.Min(memo[set, last], shortest(next, city) + cities[city].Distance(cities[last]));
                              }
                        }
                  }
                  return memo[set, last];
            }

            private class City {
                  public double latitude;
                  public double longitude;

                  public City(int latitude, int longitude) {
                        this.latitude = latitude * Math.PI / 180;
                        this.longitude = longitude * Math.PI / 180;
                  }

                  public Point GetPoint() {
                        return new Point(
                              Math.Cos(latitude) * Math.Cos(longitude),
                              Math.Cos(latitude) * Math.Sin(longitude),
                              Math.Sin(latitude));
                  }

                  public double Distance(City other) {
                        return Math.Acos(this.GetPoint().Scalar(other.GetPoint()));
                  }
            }

            private class Point {
                  public double x, y, z;

                  public Point(double x, double y, double z) {
                        this.x = x;
                        this.y = y;
                        this.z = z;
                  }

                  public double Scalar(Point other) {
                        return other.x * x + other.y * y + other.z * z;
                  }
            }
      }
}