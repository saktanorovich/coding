using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Satellites {
        public int[] detectable(string[] rockets, string[] satellites) {
            return detectable(Array.ConvertAll(
                Array.ConvertAll(rockets, rocket => rocket + " +0400.000"), Point.Parse),
                    Array.ConvertAll(satellites, Point.Parse));
        }

        private static int[] detectable(Point[] rockets, Point[] satellites) {
            var result = new List<int>();
            for (var seqno = 0; seqno < rockets.Length; ++seqno) {
                var detectedCount = satellites.Count(satellite => detectable(rockets[seqno], satellite));
                if (detectedCount > 2) {
                    result.Add(seqno);
                }
            }
            return result.ToArray();
        }

        private static bool detectable(Point rocket, Point satellite) {
            if (!rocket.Equals(satellite)) {
                var distance = MathUtils.Vector(satellite, rocket).Norm() / (satellite - rocket).Norm();
                if (MathUtils.Sign(distance - 6400) <= 0) {
                    var i = satellite.X - rocket.X;
                    var j = satellite.Y - rocket.Y;
                    var k = satellite.Z - rocket.Z;
                    var direct = new Point(i, j, k);
                    var scalar = MathUtils.Scalar(direct, -rocket) / MathUtils.Scalar(direct, direct);
                    if (0 <= MathUtils.Sign(scalar) && MathUtils.Sign(scalar - 1) <= 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        private class Point : IEquatable<Point> {
            public readonly double X;
            public readonly double Y;
            public readonly double Z;

            public Point(double x, double y, double z) {
                X = x;
                Y = y;
                Z = z;
            }

            public bool Equals(Point other) {
                return MathUtils.Sign(X - other.X) == 0 &&
                       MathUtils.Sign(Y - other.Y) == 0 &&
                       MathUtils.Sign(Z - other.Z) == 0;
            }

            public double Norm() {
                return Math.Sqrt(MathUtils.Scalar(this, this));
            }

            public static Point operator -(Point a) {
                return new Point(-a.X, -a.Y, -a.Z);
            }

            public static Point operator -(Point a, Point b) {
                return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            }

            public static Point Parse(string s) {
                var data = s.Split(' ');
                var lat = double.Parse(data[0]) * MathUtils.Pi / 180;
                var lng = double.Parse(data[1]) * MathUtils.Pi / 180;
                var alt = double.Parse(data[2]) + 6400;
                var x = alt * Math.Cos(lat) * Math.Cos(lng);
                var y = alt * Math.Cos(lat) * Math.Sin(lng);
                var z = alt * Math.Sin(lat);
                return new Point(x, y, z);
            }
        }

        private static class MathUtils {
            public const double Eps = 1e-9;
            public const double Pi = 3.1415926535897932384626433832795;

            public static int Sign(double x) {
                if (x - Eps > 0)
                    return +1;
                if (x + Eps < 0)
                    return -1;
                return 0;
            }

            public static Point Vector(Point a, Point b) {
                return new Point(
                    a.Y * b.Z - a.Z * b.Y,
                    a.Z * b.X - a.X * b.Z,
                    a.X * b.Y - a.Y * b.X);
            }

            public static double Scalar(Point a, Point b) {
                return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            }
        }
    }
}