#include <algorithm>
#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <vector>
#include <unordered_set>
using namespace std;

template<class K>
K read() {
	K value; cin >> value;
	return value;
}

struct tree_node {
	int id;
	tree_node *pp;
	tree_node *vl;
	tree_node *vr;
};

class tree {
private:
	std::vector<tree_node*> node;
	size_t size;
private:
	tree_node* make(tree_node *pp, int const &id) {
		if (id > size) {
			return nullptr;
		}
		auto nd = new tree_node();
		nd->id = id;
		nd->pp = pp;
		nd->vl = make(nd, 2 * id);
		nd->vr = make(nd, 2 * id + 1);
		node[id] = nd;
		return nd;
	}
	void swap(tree_node *v) {
		auto p = v->pp;
		if (p->id == 0) {
			return;
		}
		auto pp = p->pp;
		if (pp->vl == p) {
			pp->vl = v;
		} else {
			pp->vr = v;
		}
		v->pp = pp;
		auto vl = v->vl;
		auto vr = v->vr;
		auto pl = p->vl;
		auto pr = p->vr;
		if (p->vl == v) {
			v->vl = p;
			p->vl = vl;
			if (vl != nullptr) {
				vl->pp = p;
			}
		}
		if (p->vr == v) {
			v->vr = p;
			p->vr = vr;
			if (vr != nullptr) {
				vr->pp = p;
			}
		}
		p->pp = v;
	}
	void lvr(tree_node *node, vector<int> &path) {
		if (node == nullptr) {
			return;
		}
		lvr(node->vl, path);
		path.push_back(node->id);
		lvr(node->vr, path);
	}
public:
	tree(int const &size) {
		this->size = size;
		this->node = std::vector<tree_node*>(size + 1);
		auto root = new tree_node { 0 };
		root->vl = make(root, 1);
		node[0] = root;
	}
public:
	void swap(int const &v) {
		auto nd = node[v];
		swap(nd);
	}
	vector<int> lvr() {
		vector<int> path;
		auto root = node[0];
		lvr(root->vl, path);
		return path;
	}
};

int main() {
	auto n = read<int>();
	auto q = read<int>();
	auto t = tree(n);
	for (int i = 0; i < q; ++i) {
		auto v = read<int>();
		t.swap(v);
	}
	auto r = t.lvr();
	for (int i = 0; i < n; ++i) {
		cout << r[i] << " ";
		if (i + 1 < n) {
			cout << " ";
		}
	}
	cout << endl;
	return 0;
}