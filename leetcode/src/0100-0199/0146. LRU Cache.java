public class LRUCache {
    private final LRUListNode<Integer, Integer>[] addr;
    private final LRUList<Integer, Integer> list;
    private final int capa;

    public LRUCache(int capacity) {
        this.capa = capacity;
        this.addr = new LRUListNode[10000 + 1];
        this.list = new LRUList<>();
    }

    public int get(int key) {
        var node = warm(key);
        if (node != null) {
            return node.val;
        }
        return -1;
    }

    public void put(int key, int val) {
        var node = warm(key);
        if (node == null) {
            node = new LRUListNode<>();
            list.insert(node);
            addr[key] = node;
            if (list.size > capa) {
                var last = list.last();
                addr[last.key] = null;
                list.remove(last);
            }
        }
        node.key = key;
        node.val = val;
    }

    private LRUListNode<Integer, Integer> warm(int key) {
        var node = addr[key];
        if (node != null) {
            list.remove(node);
            list.insert(node);
        }
        return node;
    }

    private final class LRUList<K, V> {
        public LRUListNode<K, V> head;
        public LRUListNode<K, V> tail;
        public int size;

        public LRUList() {
            this.head = new LRUListNode<>();
            this.tail = head;
            this.head.next = tail;
            this.tail.prev = head;
            this.size = 0;
        }

        public void insert(LRUListNode<K, V> node) {
            head.next.prev = node;
            node.next = head.next;
            node.prev = head;
            head.next = node;
            size = size + 1;
        }

        public void remove(LRUListNode<K, V> node) {
            var prev = node.prev;
            var next = node.next;
            prev.next = next;
            next.prev = prev;
            size = size - 1;
        }

        public LRUListNode<K, V> last() {
            return tail.prev;
        }
    }

    private final class LRUListNode<K, V> {
        public LRUListNode<K, V> prev;
        public LRUListNode<K, V> next;
        public K key;
        public V val;
    }
}