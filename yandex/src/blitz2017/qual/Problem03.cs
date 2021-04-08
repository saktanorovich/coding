using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace yandex.blitz2017.qual {
    // Tournament table
    public class Problem03 {
        public bool process(int testCase, StreamReader reader, StreamWriter writer) {
            teams = new List<Team>();
            games = new List<Game>();
            while (!reader.EndOfStream) {
                var input = reader.ReadLine();
                var token = input.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                var t = new[] {
                    getTeam(token[0]),
                    getTeam(token[1])
                };
                var s = new[] {
                    int.Parse(token[2]),
                    int.Parse(token[3])
                };
                if (s[0] > s[1]) {
                    t[0].wins++;
                }
                if (s[1] > s[0]) {
                    t[1].wins++;
                }
                if (s[0] == s[1]) {
                    t[0].draw++;
                    t[1].draw++;
                }
                games.Add(new Game(t, s));
            }
            // set Index
            teams.Sort((a, b) => a.name.CompareTo(b.name));
            for (var i = 0; i < teams.Count; ++i) {
                teams[i].indx = i;
            }
            // set Rank
            teams.Sort();
            teams[0].rank = 1;
            for (var i = 1; i < teams.Count; ++i) {
                if (teams[i].CompareTo(teams[i - 1]) == 0) {
                    teams[i].rank = teams[i - 1].rank;
                } else {
                    teams[i].rank = teams[i - 1].rank + 1;
                }
            }
            writer.Write(new Table(teams, games).ToString());
            return true;
        }

        private Team getTeam(string name) {
            foreach (var t in teams) {
                if (t.name == name) {
                    return t;
                }
            }
            teams.Add(new Team(name));
            return teams.Last();
        }

        private List<Team> teams;
        private List<Game> games;

        private class Table {
            private readonly List<Team> teams;
            private readonly object[][] table;
            private readonly int[] width;

            public Table(List<Team> teams, List<Game> games) {
                this.teams = teams;
                this.table = new object[teams.Count][];
                for (var row = 0; row < teams.Count; ++row) {
                    table[row] = new object[teams.Count + 4];
                    for (var col = 0; col < table[row].Length; ++col) {
                        table[row][col] = String.Empty;
                    }
                }
                foreach (var t in teams) {
                    table[t.indx][0] = t.indx + 1;
                    table[t.indx][1] = t.name + ' ';
                    table[t.indx][teams.Count + 2] = t.score();
                    table[t.indx][t.indx + 2] = 'X';
                    if (t.rank < 4) {
                        table[t.indx][teams.Count + 3] = t.rank;
                    }
                }
                foreach (var g in games) {
                    var t0 = g.teams[0].indx;
                    var t1 = g.teams[1].indx;
                    var s0 = g.score[0];
                    var s1 = g.score[1];
                    if (s0 > s1) {
                        table[t0][t1 + 2] = 'W';
                        table[t1][t0 + 2] = 'L';
                        continue;
                    }
                    if (s1 > s0) {
                        table[t0][t1 + 2] = 'L';
                        table[t1][t0 + 2] = 'W';
                        continue;
                    }
                    table[t0][t1 + 2] = 'D';
                    table[t1][t0 + 2] = 'D';
                }
                this.width = new int[teams.Count + 4];
                for (var col = 0; col < teams.Count + 4; ++col) {
                    for (var row = 0; row < teams.Count; ++row) {
                        width[col] = Math.Max(width[col], table[row][col].ToString().Length);
                    }
                    for (var row = 0; row < teams.Count; ++row) {
                        table[row][col] = table[row][col].ToString().PadRight(width[col], ' ');
                    }
                }
            }

            public override string ToString() {
                var separator = format("+", width.Select(w => "".PadRight(w, '-')));
                var builder = new StringBuilder();
                builder.AppendLine(separator);
                for (var row = 0; row < teams.Count; ++row) {
                    builder.AppendLine(format("|", table[row]));
                    if (row + 1 < teams.Count) {
                        builder.AppendLine(separator);
                    }
                }
                builder.AppendLine(separator);
                return builder.ToString();
            }

            private string format(string d, IEnumerable<object> s) {
                return string.Format("{0}{1}{2}", d, String.Join(d, s), d);
            }
        }

        private struct Game {
            public readonly Team[] teams;
            public readonly int[] score;

            public Game(Team[] teams, int[] score) {
                this.teams = teams;
                this.score = score;
            }
        }

        private class Team : IComparable<Team> {
            public string name;
            public int indx;
            public int rank;
            public int wins;
            public int draw;

            public Team(string name) {
                this.name = name;
                this.indx = 0;
                this.rank = 0;
                this.wins = 0;
                this.draw = 0;
            }

            public int score() {
                return 3 * wins + draw;
            }

            public int CompareTo(Team other) {
                if (score() != other.score()) {
                    return -1 * (score() - other.score());
                }
                return -1 * (wins - other.wins);
            }
        }
    }
}
