using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2353 {
        public class FoodRatings {
            private readonly Dictionary<string, SortedSet<(int, string)>> cuisines;
            private readonly Dictionary<string, string> foods;
            private readonly Dictionary<string, int> ratings;

            public FoodRatings(string[] foods, string[] cuisines, int[] ratings) {
                this.cuisines = new Dictionary<string, SortedSet<(int, string)>>();
                this.foods = new Dictionary<string, string>();
                this.ratings = new Dictionary<string, int>();
                for (var i = 0; i < cuisines.Length; ++i) {
                    if (this.cuisines.ContainsKey(cuisines[i]) == false) {
                        this.cuisines.Add(cuisines[i], new SortedSet<(int, string)>());
                    }
                    this.cuisines[cuisines[i]].Add((-ratings[i], foods[i]));
                    this.foods[foods[i]] = cuisines[i];
                    this.ratings[foods[i]] = ratings[i];
                }
            }
            
            public void ChangeRating(string food, int rating) {
                var cuisien = foods[food];
                cuisines[cuisien].Remove((-ratings[food], food));
                cuisines[cuisien].Add((-rating, food));
                ratings[food] = rating;
            }
            
            public string HighestRated(string cuisine) {
                var foods = cuisines[cuisine];
                return foods.Min.Item2;
            }
        }
    }
}
