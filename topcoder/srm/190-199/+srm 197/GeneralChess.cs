using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class GeneralChess {
        public string[] attackPositions(string[] pieces) {
            return attackPositions(Array.ConvertAll(pieces, piece => {
                var data = piece.Split(',');
                var x = int.Parse(data[0]);
                var y = int.Parse(data[1]);
                return new Pos(x, y);
            }));
        }

        private static string[] attackPositions(Pos[] pieces) {
            var result = new List<Pos>();
            for (var i = 0; i < 8; ++i) {
                var from = new Pos(pieces[0].X + dx[i], pieces[0].Y + dy[i]);
                if (match(pieces, from)) {
                    result.Add(from);
                }
            }
            return result.Select(x => x.ToString()).ToArray();
        }

        private static bool match(Pos[] pieces, Pos from) {
            foreach (var piece in pieces) {
                var dx = Math.Abs(piece.X - from.X);
                var dy = Math.Abs(piece.Y - from.Y);
                if (dx == 2 && dy == 1 ||
                    dx == 1 && dy == 2) {
                    continue;
                }
                return false;
            }
            return true;
        }

        private static readonly int[] dx = { -2, -2, -1, -1, +1, +1, +2, +2 };
        private static readonly int[] dy = { -1, +1, -2, +2, -2, +2, -1, +1 };

        private struct Pos : IComparable<Pos> {
            public readonly int X;
            public readonly int Y;

            public Pos(int x, int y) {
                X = x;
                Y = y;
            }

            public override string ToString() {
                return string.Format("{0},{1}", X, Y);
            }

            public int CompareTo(Pos other) {
                if (X != other.X) {
                    return X - other.X;
                }
                return Y - other.Y;
            }
        }
    }
}