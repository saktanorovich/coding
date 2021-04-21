using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2034 {
        public class StockPrice {
            private readonly Dictionary<int, int> prices;
            private readonly SortedSet<(int, int)> records;
            private int currentTime;

            public StockPrice() {
                prices = new Dictionary<int, int>();
                records = new SortedSet<(int, int)>();
            }
            
            public void Update(int timestamp, int price) {
                currentTime = Math.Max(currentTime, timestamp);
                if (prices.TryGetValue(timestamp, out var oldPrice)) {
                    records.Remove((oldPrice, timestamp));
                }
                prices[timestamp] = price;
                records.Add((price, timestamp));
            }
            
            public int Current() {
                return prices[currentTime];
            }
            
            public int Maximum() {
                return records.Max.Item1;
            }
            
            public int Minimum() {
                return records.Min.Item1;
            }
        }
    }
}
