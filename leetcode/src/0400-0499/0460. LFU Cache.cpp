template<class K, class V>
class LFUCacheImpl {
private:
    int capa = 0;
    int time = 0;
    std::unordered_map<K, int> used;
    std::unordered_map<K, V> vals;
    std::unordered_map<K, int> freq;
    std::set<std::pair<std::pair<int, int>, K>> list;
private:
    inline std::pair<std::pair<int, int>, K> make(const K& key) {
        return std::make_pair(std::make_pair(freq[key], used[key]), key);
    }
public:
    LFUCacheImpl(const int& capacity) {
        this->capa = capacity;
    }

    V get(const K& key) {
        if (!vals.count(key)) {
            return -1;
        }
        auto entryOld = make(key);
        list.erase(entryOld);
        freq[key] ++;
        used[key] = time ++;
        auto entryNew = make(key);
        list.insert(entryNew);
        return vals[key];
    }

    void put(const K& key, const V& value) {
        if (vals.count(key)) {
            auto entryOld = make(key);
            list.erase(entryOld);
            freq[key] ++;
            used[key] = time ++;
            vals[key] = value;
            auto entryNew = make(key);
            list.insert(entryNew);
        } else {
            auto have = list.size();
            if (have == capa) {
                auto [_, recent] = *list.begin();
                auto remove = make(recent);
                list.erase(remove);
                vals.erase(recent);
                used.erase(recent);
                freq.erase(recent);
            }
            freq[key] = 1;
            used[key] = time ++;
            vals[key] = value;
            list.insert(make(key));
        }
    }
};

using LFUCache = LFUCacheImpl<int, int>;
