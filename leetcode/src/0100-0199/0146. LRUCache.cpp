template<class K, class V>
class LRUCacheImpl {
private:
    int capa = 0;
    int time = 0;
    std::unordered_map<K, int> used;
    std::unordered_map<K, V> vals;
    std::set<pair<int, K>> list;
public:
    LRUCacheImpl(int capacity) {
        this->capa = capacity;
    }

    V get(K key) {
        if (!vals.count(key)) {
            return -1;
        }
        list.erase(  { used[key], key });
        used[key] = time ++;
        list.insert( { used[key], key });
        return vals[key];
    }

    void put(K key, V value) {
        if (vals.count(key)) {
            list.erase({ used[key], key });
            vals.erase(key);
            used.erase(key);
        }
        auto have = list.size();
        if (have == capa) {
            auto [_, recent] = *list.begin();
            list.erase({ used[recent], recent });
            used.erase(recent);
            vals.erase(recent);
        }
        used[key] = time ++;
        vals[key] = value;
        list.insert({ used[key], key });
    }
};

using LRUCache = LRUCacheImpl<int, int>;