#include <iostream>
using namespace std;

int const max_n = 52;
int const oo = +1000000000;

int nroads;
int nitems_to_deliver;
bool g[max_n][max_n];
int starting_place, place_of_delivery;
bool visited[max_n];
int min_items[max_n];

int get_code(char c) {
    if ('a' <= c && c <= 'z') {
        return c - 'a';
    }
    return c - 'A' + 26;
}

int toll_to_pay(int source, int destination) {
    if (source == destination) {
        return 0;
    }
    if (destination < 26) {
        return 1;
    }
    int destination_items = min_items[destination];
    int source_items = destination_items;
    while (true) {
        ++source_items;
        int pay = source_items / 20;
        if (pay * 20 < source_items) {
            ++pay;
        }
        if (source_items - pay == destination_items) {
            break;
        }
    }
    return source_items - destination_items;
}

int main() {
    for (int case_id = 1; cin >> nroads; ++case_id) {
        if (nroads < 0) {
            break;
        }
        for (int i = 0; i < max_n; ++i) {
            for (int j = 0; j < max_n; ++j) {
                g[i][j] = false;
            }
        }
        cin.get();
        char c1, c2;
        for (int end1, end2, i = 0; i < nroads; ++i) {
            cin >> c1 >> c2;
            cin.get();
            end1 = get_code(c1);
            end2 = get_code(c2);
            g[end1][end2] = g[end2][end1] = true;
        }
        cin >> nitems_to_deliver >> c1 >> c2;
        starting_place = get_code(c1);
        place_of_delivery = get_code(c2);
        cin.get();
        for (int i = 0; i < max_n; ++i) {
            visited[i] = false;
            min_items[i] = +oo;
        }
        min_items[place_of_delivery] = nitems_to_deliver;
        while (true) {
            int current_min_items = +oo, index = -1;
            for (int i = 0; i < max_n; ++i) {
                if (!visited[i]) {
                    if (min_items[i] < current_min_items) {
                        current_min_items = min_items[i];
                        index = i;
                    }
                }
            }
            if (index == -1) {
                break;
            }
            visited[index] = true;
            for (int i = 0; i < max_n; ++i) {
                if (g[i][index]) {
                    int items = toll_to_pay(i, index) + current_min_items;
                    if (items < min_items[i]) {
                        min_items[i] = items;
                        visited[i] = false;
                    }
                }
            }
        }
        cout << "Case " << case_id << ": " << min_items[starting_place] << endl;
    }
    return 0;
}
