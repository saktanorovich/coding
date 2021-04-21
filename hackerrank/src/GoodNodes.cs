/**
We have a list of N nodes with each node pointing to one of
the N nodes. It could even be pointing to itself. We call a
node ‘good’, if it satisfies one of the following properties:

* It is the tail node (marked as node 1)
* It is pointing to the tail node (node 1)
* It is pointing to a good node

You can change the pointers of some nodes in order to make
them all ‘good’. You are given the description of the nodes.
You have to find out what is minimum number of nodes that
you have to change in order to make all the nodes good.

Input
The first line of input contains an integer number N
which is the number of nodes. The next N lines contains N numbers,
all between 1 and N. The first number is the number of the node
pointed to by node 1; the second number is the number of the node
pointed to by node 2; the third number is the number of the node
pointed to by Node 3 and so on.

Output
Print a single integer which is the answer to the problem.

Constraints: N is no larger than 1000.

Sample Input
5
1
2
3
4
5

Sample output
4
*/
using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class GoodNodes {
        public int count(int n, int[] reference) {
            return countImpl(n, Array.ConvertAll(reference, @ref => @ref - 1));
        }

        private int countImpl(int n, int[] reference) {
            if (reach(reference[0], 0, reference, new bool[n])) {
                return componentsCount(n, reference) - 1;
            }
            return componentsCount(n, reference);
        }

        private bool reach(int curr, int goal, int[] reference, bool[] visited) {
            if (curr != goal) {
                visited[curr] = true;
                if (!visited[reference[curr]]) {
                    return reach(reference[curr], goal, reference, visited);
                }
                return false;
            }
            return true;
        }

        private int componentsCount(int n, int[] reference) {
            var graph = Array.ConvertAll(new int[n], x => new List<int>());
            for (var i = 0; i < n; ++i) {
                graph[i].Add(reference[i]);
                graph[reference[i]].Add(i);
            }
            var result = 0;
            var visited = new bool[n];
            for (var curr = 0; curr < n; ++curr) {
                if (!visited[curr]) {
                    dfs(curr, graph, visited);
                    result = result + 1;
                }
            }
            return result;
        }

        private void dfs(int curr, List<int>[] graph, bool[] visited) {
            visited[curr] = true;
            foreach (var next in graph[curr]) {
                if (!visited[next]) {
                    dfs(next, graph, visited);
                }
            }
        }
    }
}
