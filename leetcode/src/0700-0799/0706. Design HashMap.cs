using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0706 {
        public class MyHashMap {
            private const ulong mult = 0x9E3779B97F4A8000UL;
            private const int empty = -1;
            private const int size = 1 << 20;
            private const int mask = size - 2;
            private const int rsh = 44;
            private readonly int[] store = new int[2 * size];

            public MyHashMap() {
                for (var i = 0; i < 2 * size; ++i) {
                    store[i] = empty;
                }
            }
            
            public void Put(int key, int value) {
                var index = idx(key);
                store[index] = key;
                store[index + 1] = value;
            }
            
            public int Get(int key) {
                return store[idx(key) + 1];
            }
            
            public void Remove(int key) {
                var index = idx(key);
                store[index] = empty;
                store[index + 1] = empty;
            }

            private int hash(int key) {
                var h = (ulong)key;
                h = h * mult;
                h = h >> rsh;
                h = h & mask;
                return (int)h;
            }

            private int idx(int key) {
                var index = hash(key);
                for (var slot = store[index]; slot != key;) {
                    if (slot == empty) {
                        break;
                    }
                    if (index == 0) {
                        index = size;
                    }
                    index -= 2;
                    slot = store[index];
                }
                return index;
            }
        }
    }
}
