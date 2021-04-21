class Solution {
    public String frequencySort(String s) {
        var freq = new HashMap<Character, Integer>();
        for (var c : s.toCharArray()) {
            var have = freq.getOrDefault(c, 0);
            freq.put(c, have + 1);
        }
        var heap = new PriorityQueue<Character>(
            (a, b) -> freq.get(b) - freq.get(a)
        );
        for (var c : freq.keySet()) {
            heap.add(c);
        }
        var answ = new StringBuilder();
        while (heap.isEmpty() == false) {
            var c = heap.poll();
            var f = freq.get(c);
            answ.append(String.valueOf(c).repeat(f));
        }
        return answ.toString();
    }
}