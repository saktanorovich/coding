using System;

namespace TopCoder.Algorithm {
    public class CuboidJoin {
        public long totalVolume(int[] coords) {
            var cuboids = new Cuboid[coords.Length / 6];
            for (var i = 0; i < coords.Length; i += 6) {
                cuboids[i / 6] = new Cuboid(
                    coords[i + 0],
                    coords[i + 1],
                    coords[i + 2],
                    coords[i + 3],
                    coords[i + 4],
                    coords[i + 5]
                );
            }
            return totalVolume(cuboids);
        }

        private static long totalVolume(Cuboid[] cuboids) {
            var result = 0L;
            for (int set = 1, sign = -1; set < 1 << cuboids.Length; ++set, sign = -1) {
                var cuboid = new Cuboid(-5000, -5000, -5000, +5000, +5000, +5000);
                for (var i = 0; i < cuboids.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        cuboid = cuboid.Intersect(cuboids[i]);
                        sign = -sign;
                    }
                }
                result += sign * cuboid.Volume;
            }
            return result;
        }

        private struct Cuboid {
            public readonly int X0, Y0, Z0;
            public readonly int X1, Y1, Z1;

            public Cuboid(int x0, int y0, int z0, int x1, int y1, int z1) {
                X0 = x0;
                Y0 = y0;
                Z0 = z0;
                X1 = x1;
                Y1 = y1;
                Z1 = z1;
            }

            public long Volume {
                get {
                    if (X0 <= X1 && Y0 <= Y1 && Z0 <= Z1) {
                        return 1L * (X1 - X0) * (Y1 - Y0) * (Z1 - Z0);
                    }
                    return 0;
                }
            }

            public Cuboid Intersect(Cuboid cuboid) {
                var x0 = Math.Max(X0, cuboid.X0);
                var y0 = Math.Max(Y0, cuboid.Y0);
                var z0 = Math.Max(Z0, cuboid.Z0);
                var x1 = Math.Min(X1, cuboid.X1);
                var y1 = Math.Min(Y1, cuboid.Y1);
                var z1 = Math.Min(Z1, cuboid.Z1);
                return new Cuboid(x0, y0, z0, x1, y1, z1);
            }
        }
    }
}