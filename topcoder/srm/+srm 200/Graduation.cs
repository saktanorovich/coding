using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Graduation {
        public string moreClasses(string classesTaken, string[] requirements) {
            return moreClasses(parse(classesTaken), parse(requirements));
        }

        private string moreClasses(IList<int> classesTaken, IList<IList<int>> requirements) {
            var classesRequired = 0;
            for (var i = 0; i < requirements.Count; ++i) {
                var required = requirements[i][0];
                capacity[numOfClasses + i, numOfVertices - 1] = required;
                classesRequired += required;
                requirements[i].RemoveAt(0);
            }
            foreach (var classe in classesTaken) {
                addEdges(requirements, classe, 1);
            }
            var result = string.Empty;
            var maximumFlow = getMaximumFlow();
            for (var classe = 33; maximumFlow < classesRequired && classe < numOfClasses; ++classe) {
                if (char.IsDigit((char)classe) || capacity[0, classe] > 0) {
                    continue;
                }
                addEdges(requirements, classe, 1);
                if (maximumFlow + 1 == getMaximumFlow()) {
                    maximumFlow = maximumFlow + 1;
                    result += (char)classe;
                    continue;
                }
                addEdges(requirements, classe, 0);
            }
            if (maximumFlow == classesRequired) {
                return result;
            }
            return "0";
        }

        private void addEdges(IList<IList<int>> requirements, int classe, int value) {
            capacity[0, classe] = value;
            for (var i = 0; i < requirements.Count; ++i) {
                if (requirements[i].Contains(classe)) {
                    capacity[classe, numOfClasses + i] = value;
                }
            }
        }

        private int getMaximumFlow() {
            var result = 0;
            for (var flow = new int[numOfVertices, numOfVertices];;) {
                if (augment(0, numOfVertices - 1, flow, new bool[numOfVertices])) {
                    result = result + 1;
                }
                else break;
            }
            return result;
        }

        private bool augment(int source, int target, int[,] flow, bool[] visited) {
            if (!visited[source]) {
                if (source == target) {
                    return true;
                }
                visited[source] = true;
                for (var next = 0; next < numOfVertices; ++next) {
                    if (flow[source, next] < capacity[source, next]) {
                        if (augment(next, target, flow, visited)) {
                            ++flow[source, next];
                            --flow[next, source];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private const int numOfVertices = 200;
        private const int numOfClasses = 128;

        private readonly int[,] capacity = new int[numOfVertices, numOfVertices];

        private static IList<IList<int>> parse(string[] requirements) {
            var result = new List<IList<int>>();
            for (var i = 0; i < requirements.Length; ++i) {
                var decoded = new List<int> { 0 };
                for (var j = 0; j < requirements[i].Length; ++j) {
                    if (char.IsDigit(requirements[i][j])) {
                        decoded[0] = decoded[0] * 10 + (requirements[i][j] - '0');
                    }
                    else {
                        decoded.Add((int)requirements[i][j]);
                    }
                }
                result.Add(decoded);
            }
            return result;
        }

        private static int[] parse(string classes) {
            return Array.ConvertAll(classes.ToCharArray(), c => (int)c);
        }
    }
}