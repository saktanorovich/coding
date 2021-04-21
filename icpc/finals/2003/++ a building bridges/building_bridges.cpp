#include <cstdlib>
#include <iostream>
#include <vector>

using namespace std;

int const max_nrows = 100;
int const max_ncolumns = 100;
int const max_nblocks = max_nrows * max_ncolumns;
int const max_nedges = 6 * max_nblocks;
int const oo = +1000000000;
int const undefined = - 1;

struct tblock {
    int row, column;
    tblock() {
        this->row = undefined;
        this->column = undefined;
    }
};

struct tedge {
    int source, destination;
    int length;
};

int edges_comparer(void const *e1, void const *e2) {
    return ((tedge*)e1)->length - ((tedge*)e2)->length;
}

int parent[max_nblocks];
int rank[max_nblocks];
tblock blocks[max_nblocks];
int block_index[max_ncolumns][max_ncolumns];
int nearest_in_row[max_nrows][max_ncolumns];
int nearest_in_column[max_nrows][max_ncolumns];
tedge edges[max_nedges + 1];
int nedges, ngroups;

void link(int node1, int node2) {
    if (node1 != node2) {
        --ngroups;
    }
    if (rank[node1] > rank[node2]) {
        parent[node2] = node1;
    }
    else {
        parent[node1] = node2;
        if (rank[node1] == rank[node2]) {
            ++rank[node2];
        }
    }
}

int get_set(int node) {
    if (parent[node] != node) {
        parent[node] = get_set(parent[node]);
    }
    return parent[node];
}

void join(int node1, int node2) {
    link(get_set(node1), get_set(node2));
}

int calc_distance(tblock const *block1, tblock const *block2) {
    int r_distance = max(abs(block1->row - block2->row) - 1, 0);
    int c_distance = max(abs(block1->column - block2->column) - 1, 0);
    int min_distance = min(r_distance, c_distance);
    int max_distance = max(r_distance, c_distance);
    if (min_distance == 0) {
        return max_distance;
    }
    return +oo;
}

void process_block(int analysed_index, int index) {
    if (index != undefined) {
        if (get_set(analysed_index) != get_set(index)) {
            int d = calc_distance(&blocks[analysed_index], &blocks[index]);
            if (d == 0) {
                join(analysed_index, index);
            }
            else if (d < +oo) {
                edges[nedges].source = analysed_index;
                edges[nedges].destination = index;
                edges[nedges].length = d;
                ++nedges;
            }
        }
    }
}

int main() {
    for (int case_id = 1, nrows, ncolumns; cin >> nrows >> ncolumns; ++case_id) {
        if (nrows <= 0 || ncolumns <= 0) {
            break;
        }
        if (case_id > 1) {
            cout << endl;
        }
        cin.get();
        int nblocks = 0;
        for (int i = 0; i < nrows; ++i) {
            for (int j = 0; j < ncolumns; ++j) {
                char mark = cin.get();
                nearest_in_row[i][j] = undefined;
                nearest_in_column[i][j] = undefined;
                block_index[i][j] = undefined;
                if (mark == '#') {
                    block_index[i][j] = nblocks;
                    blocks[nblocks].row = i;
                    blocks[nblocks].column = j;
                    ++nblocks;
                }
                if (i > 0) {
                    nearest_in_column[i][j] = (block_index[i - 1][j] != undefined ? block_index[i - 1][j] : nearest_in_column[i - 1][j]);
                }
                if (j > 0) {
                    nearest_in_row[i][j] = (block_index[i][j - 1] != undefined ? block_index[i][j - 1] : nearest_in_row[i][j - 1]);
                }
            }
            cin.get();
        }
        for (int i = 0; i < nblocks; ++i) {
            parent[i] = i;
            rank[i] = 0;
        }
        ngroups = nblocks;
        nedges = 0;
        for (int i = 0; i < nblocks; ++i) {
            int r = blocks[i].row;
            int c = blocks[i].column;
            for (int j = max(0, r - 1); j <= min(r + 1, nrows - 1); ++j) {
                process_block(i, nearest_in_row[j][c]);
            }
            for (int j = max(0, c - 1); j <= min(c + 1, ncolumns - 1); ++j) {
                process_block(i, nearest_in_column[r][j]);
            }
        }
        int total_bridges_count = 0;
        int total_bridges_length = 0;
        qsort(edges, nedges, sizeof(tedge), edges_comparer);
        for (int i = 0; i < nedges; ++i) {
            tedge &e = edges[i];
            if (get_set(e.source) != get_set(e.destination)) {
                join(e.source, e.destination);
                if (e.length > 0) {
                    ++total_bridges_count;
                    total_bridges_length += e.length;
                }
            }
        }
        cout << "City " << case_id << endl;
        if (total_bridges_length == 0) {
            cout << (ngroups > 1 ? "No bridges are possible." : "No bridges are needed.") << endl;
        }
        else {
            cout << total_bridges_count;
            cout << (total_bridges_count > 1 ? " bridges" : " bridge") << " of total length " << total_bridges_length << endl;
        }
        if (ngroups > 1) {
            cout << ngroups << " disconnected groups" << endl;
        }
    }
    return 0;
}
