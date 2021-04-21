using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0721 {
        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts) {
            var emailToAccount = new Dictionary<string, List<int>>();
            for (var a = 0; a < accounts.Count; ++a) {
                for (var i = 1; i < accounts[a].Count; ++i) {
                    var email = accounts[a][i];
                    if (emailToAccount.ContainsKey(email) == false) {
                        emailToAccount.Add(email, new List<int>());
                    }
                    emailToAccount[email].Add(a);
                }
            }
            var lead = new int[accounts.Count];
            var list = new List<int>[accounts.Count];
            for (var a = 0; a < accounts.Count; ++a) {
                lead[a] = a;
                list[a] = new List<int> { a };
            }
            foreach (var set in emailToAccount.Values) {
                var x = set[0];
                for (var i = 1; i < set.Count; ++i) {
                    var y = set[i];
                    if (lead[x] != lead[y]) {
                        var u = lead[x];
                        var v = lead[y];
                        if (list[u].Count >= list[v].Count) {
                            foreach (var z in list[v]) {
                                lead[z] = u;
                                list[u].Add(z);
                            }
                            list[v].Clear();
                        } else {
                            foreach (var z in list[u]) {
                                lead[z] = v;
                                list[v].Add(z);
                            }
                            list[u].Clear();
                        }
                    }
                }
            }
            var merged = new List<IList<string>>();
            for (var a = 0; a < accounts.Count; ++a) {
                if (list[a].Count > 0) {
                    var emails = new SortedSet<string>(StringComparer.Ordinal);
                    foreach (var x in list[a]) {
                        for (var i = 1; i < accounts[x].Count; ++i) {
                            emails.Add(accounts[x][i]);
                        }
                    }
                    var person = new List<string>();
                    person.Add(accounts[a][0]);
                    person.AddRange(emails);
                    merged.Add(person);
                }
            }
            return merged;
        }
    }
}
