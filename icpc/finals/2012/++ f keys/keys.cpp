#include <cstdio>
#include <iostream>
#include <queue>
#include <sstream>
#include <string>
#include <vector>
using namespace std;

int const max_vertices_count = 30;
int const oo = +1000000000;

struct edge {
      int src, dst;
      int capacity, flow;
      edge *next, *reverse;
      edge(int src, int dst, int capacity) {
            this->src = src;
            this->dst = dst;
            this->capacity = capacity;
            this->flow = 0;
            next = reverse = 0;
      }
};

typedef edge* graph[max_vertices_count + 1];

void add_edge(graph g, int source, int destination, int capacity) {
      edge *e = new edge(source, destination, capacity);
      e->next = g[source];
      g[source] = e;
}

class max_flow_algorithm {
private:
      int _visited[max_vertices_count + 1];
private:
      int augment(graph const &g, int const &source, int const &sink, int flow) {
            _visited[source] = 1;
            if (source == sink) {
                  return flow;
            }
            for (edge *e = g[source]; e != 0; e = e->next) {
                  if (!_visited[e->dst]) {
                        if (e->flow < e->capacity) {
                              int by = augment(g, e->dst, sink, min(flow, e->capacity - e->flow));
                              if (by > 0) {
                                    e->flow += by;
                                    if (e->reverse != 0) {
                                          e->reverse->flow -= by;
                                    }
                                    return by;
                              }
                        }
                  }
            }
            return 0;
      }
public:
      int get_max_flow(graph const &g, int vertices_count, int const &source, int const &sink) {
            int max_flow = 0;
            while (true) {
                  for (int i = 0; i <= vertices_count; ++i) {
                        _visited[i] = 0;
                  }
                  int flow = augment(g, source, sink, +oo);
                  if (flow == 0) {
                        break;
                  }
                  max_flow += flow;
            }
            return max_flow;
      }
};

void __assert(bool condition, string message) {
      if (!condition) {
            cerr << message << endl;
            throw message;
      }
}

istringstream* get_line() {
      static char buffer[256];
      cin.getline(buffer, 256);
      return new istringstream(buffer);
}

struct ring {
public:
      int keys_count[2];
public:
      ring() {
            keys_count[0] = keys_count[1] = 0;
      }
      ring(ring* const r) {
            keys_count[0] = r->keys_count[0];
            keys_count[1] = r->keys_count[1];
      }
};

class processor {
private:
      void dfs(graph const &g, int source, vector<int> &visited) {
            visited[source] = 1;
            for (edge *e = g[source]; e != 0; e = e->next) {
                  if (!visited[e->dst]) {
                        dfs(g, e->dst, visited);
                  }
            }
      }
      void build_cut(graph const &g, int source, int &cut, vector<int> &visited) {
            visited[source] = 1;
            cut |= (1 << source);
            for (edge *e = g[source]; e != 0; e = e->next) {
                  if (!visited[e->dst]) {
                        if (e->flow < e->capacity) {
                              build_cut(g, e->dst, cut, visited);
                        }
                  }
            }
      }
      int get_min_rings_operations_count(vector<vector<int> > const &rings_graph, vector<ring*> const &rings) {
            for (int i = 0; i < 26; ++i) {
                  if (rings[i]) {
                        __assert(!(rings[i]->keys_count[0] > 0 && rings[i]->keys_count[1] > 0), "the ring should not contain both key types");
                  }
            }
            int result = 0;
            graph fgraph = { 0 };
            graph separated = { 0 };
            int source = 26, sink = 27;
            for (int a = 0; a < 26; ++a) {
                  for (int b = a + 1; b < 26; ++b) {
                        if (rings_graph[a][b] > 0) {
                              __assert(rings[a] != (ring*)0, "the ring a is missed during processing");
                              __assert(rings[b] != (ring*)0, "the ring b is missed during processing");
                              if (rings[a]->keys_count[0] > 0) add_edge(fgraph, source, a, +oo);
                              if (rings[b]->keys_count[0] > 0) add_edge(fgraph, source, b, +oo);
                              if (rings[a]->keys_count[1] > 0) add_edge(fgraph, a, sink, +oo);
                              if (rings[b]->keys_count[1] > 0) add_edge(fgraph, b, sink, +oo);
                              add_edge(fgraph, a, b, +1);
                              add_edge(fgraph, b, a, +1);
                              fgraph[a]->reverse = fgraph[b];
                              fgraph[b]->reverse = fgraph[a];
                        }
                  }
            }
            result += (new max_flow_algorithm())->get_max_flow(fgraph, 27, source, sink);
            vector<int> visited(27, 0);
            int cut = 0;
            build_cut(fgraph, source, cut, visited);
            for (int i = 0; i < 26; ++i) {
                  if (rings[i]) {
                        for (edge *e = fgraph[i]; e != 0; e = e->next) {
                              int src_in_cut = (cut & (1 << e->src)) != 0;
                              int dst_in_cut = (cut & (1 << e->dst)) != 0;
                              if (src_in_cut + dst_in_cut == 1) {
                                    continue;
                              }
                              if (e->src == source || e->src == sink) continue;
                              if (e->dst == source || e->dst == sink) continue;
                              add_edge(separated, e->src, e->dst, 0);
                              add_edge(separated, e->dst, e->src, 0);
                        }
                  }
            }
            int keys_components_count[2] = { 0, 0 };
            visited.assign(26, 0);
            for (int i = 0; i < 26; ++i) {
                  if (rings[i]) {
                        if (!visited[i]) {
                              if (rings[i]->keys_count[0] + rings[i]->keys_count[1] > 0) {
                                    dfs(separated, i, visited);
                                    if (rings[i]->keys_count[0] > 0) ++keys_components_count[0];
                                    if (rings[i]->keys_count[1] > 0) ++keys_components_count[1];
                              }
                        }
                  }
            }
            if (keys_components_count[0] > 1) result += keys_components_count[0] - 1;
            if (keys_components_count[1] > 1) result += keys_components_count[1] - 1;
            return result;
      }
      void relax(pair<int, int> &res, int keys_operations_count, vector<vector<int> > const &rings_graph, vector<ring*> const &rings) {
            if (res.first > keys_operations_count) {
                  res.first  = keys_operations_count;
                  res.second = get_min_rings_operations_count(rings_graph, rings);
            }
            else if (res.first == keys_operations_count) {
                  res.second = min(res.second, get_min_rings_operations_count(rings_graph, rings));
            }
      }
public:
      pair<int, int> process(vector<vector<int> > const &rings_graph, vector<int> const &key_attached_to_ring) {
            int keys_count[2] = { 0, 0 };
            for (int i = 0; i < 26; ++i) {
                  if (key_attached_to_ring[i] != -1) {
                        ++keys_count[i < 13 ? 0 : 1];
                  }
            }
            if (keys_count[0] + keys_count[1] == 0) {
                  return make_pair(0, 0);
            }
            vector<ring*> rings(26);
            for (int i = 0; i < 26; ++i) {
                  for (int j = i + 1; j < 26; ++j) {
                        if (rings_graph[i][j]) {
                              if (!rings[i]) rings[i] = new ring();
                              if (!rings[j]) rings[j] = new ring();
                        }
                  }
            }
            int keys_on_the_rings[2] = { 0, 0 };
            for (int i = 0; i < 26; ++i) {
                  if (key_attached_to_ring[i] != -1) {
                        if (!rings[key_attached_to_ring[i]]) {
                              rings[key_attached_to_ring[i]] = new ring();
                        }
                        ++rings[key_attached_to_ring[i]]->keys_count[i < 13 ? 0 : 1];
                        keys_on_the_rings[i < 13 ? 0 : 1] |= (1 << key_attached_to_ring[i]);
                  }
            }
            vector<int> rings_with_both_keys;
            int rings_with_both_keys_set = 0;
            for (int i = 0; i < 26; ++i) {
                  if (rings[i]) {
                        if (rings[i]->keys_count[0] > 0 && rings[i]->keys_count[1] > 0) {
                              rings_with_both_keys.push_back(i);
                              rings_with_both_keys_set |= (1 << i);
                        }
                  }
            }
            pair<int, int> result = make_pair(0, 0);
            if (rings_with_both_keys.size() > 0) {
                  __assert(rings_with_both_keys.size() <= 13, "the number of rings with both key types should not exceed 13");
                  result.first  = +oo;
                  result.second = +oo;
                  for (int set = 0; set < (1 << rings_with_both_keys.size()); ++set) {
                        vector<ring*> current_rings(26);
                        for (int i = 0; i < 26; ++i) {
                              if (rings[i]) {
                                    current_rings[i] = new ring(rings[i]);
                              }
                        }
                        int current_keys_on_the_rings[2] = { keys_on_the_rings[0], keys_on_the_rings[1] };
                        int keys_operations_count[2] = { 0, 0 };
                        for (size_t i = 0; i < rings_with_both_keys.size(); ++i) {
                              int ring_no = rings_with_both_keys[i];
                              int key = (set & (1 << i)) ? 0 : 1;
                              keys_operations_count[key] += rings[ring_no]->keys_count[key];
                              current_rings[ring_no]->keys_count[key] = 0;
                              current_keys_on_the_rings[key] ^= (1 << ring_no);
                        }
                        if (current_keys_on_the_rings[0] > 0 && current_keys_on_the_rings[1] > 0) {
                              relax(result, 2 * (keys_operations_count[0] + keys_operations_count[1]), rings_graph, current_rings);
                        }
                        else {
                              __assert(current_keys_on_the_rings[0] > 0 || current_keys_on_the_rings[1] > 0, "unexpected error during processing rings with both key types");
                              int key = (current_keys_on_the_rings[0] == 0 ? 0 : 1);
                              for (int i = 0; i < 26; ++i) {
                                    if (current_rings[i] && (rings_with_both_keys_set & (1 << i)) == 0) {
                                          int temp = current_rings[i]->keys_count[1 - key];
                                          current_rings[i]->keys_count[key] = keys_operations_count[key];
                                          current_rings[i]->keys_count[1 - key] = 0;
                                          keys_operations_count[1 - key] += temp;
                                          relax(result, 2 * (keys_operations_count[0] + keys_operations_count[1]), rings_graph, current_rings);
                                          current_rings[i]->keys_count[key] = 0;
                                          current_rings[i]->keys_count[1 - key] = temp;
                                          keys_operations_count[1 - key] -= temp;
                                    }
                              }
                        }
                  }
            }
            else {
                  result.second = get_min_rings_operations_count(rings_graph, rings);
            }
            return result;
      }
};

int main() {
      for (int test_case = 1; true; ++test_case) {
            vector<vector<int> > rings_graph(26, vector<int>(26));
            vector<int> key_attached_to_ring(max_vertices_count, -1);
            string descriptor;
            if (*get_line() >> descriptor) {
                  do {
                        if (descriptor.size() > 1) {
                              if (descriptor[0] > descriptor[1]) {
                                    std::swap(descriptor[0], descriptor[1]);
                              }
                              bool is_key0 = ('A' <= descriptor[0] && descriptor[0] <= 'Z');
                              bool is_key1 = ('A' <= descriptor[1] && descriptor[1] <= 'Z');
                              __assert(!(is_key0 && is_key1), "two keys should not be connected to each other");
                              if (is_key0) {
                                    key_attached_to_ring[descriptor[0] - 'A'] = descriptor[1] - 'a';
                                    goto next;
                              }
                              if (is_key1) {
                                    key_attached_to_ring[descriptor[1] - 'A'] = descriptor[0] - 'a';
                                    goto next;
                              }
                              rings_graph[descriptor[0] - 'a'][descriptor[1] - 'a'] = 1;
                              rings_graph[descriptor[1] - 'a'][descriptor[0] - 'a'] = 1;
                        }
                        else {
                              break;
                        }
                        next:;
                  } while (*get_line() >> descriptor);
                  pair<int, int> result = (new processor())->process(rings_graph, key_attached_to_ring);
                  cout << "Case " << test_case << ": ";
                  if (result.first < +oo) {
                        cout << result.first << ' ' << result.second << endl;
                  }
                  else {
                        cout << "impossible" << endl;
                  }
            }
            else {
                  break;
            }
      }
      cout.flush();
      return 0;
}
