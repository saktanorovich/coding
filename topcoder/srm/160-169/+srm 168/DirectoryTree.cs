using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class DirectoryTree {
            public string[] display(string[] files) {
                  result.Add("ROOT");
                  build("/", files, "");
                  return result.ToArray();
            }
            private void build(string directory, string[] paths, string indent) {
                  List<string> children = new List<string>();
                  foreach (string path in paths) {
                        if (path.StartsWith(directory)) {
                              string child = path.Substring(directory.Length).Split('/')[0];
                              if (!children.Contains(child)) {
                                    children.Add(child);
                              }
                        }
                  }
                  children.Sort();
                  for (int i = 0; i < children.Count; ++i) {
                        result.Add(string.Format("{0}+-{1}", indent, children[i]));
                        string next = indent + "  ";
                        if (i + 1 < children.Count) {
                              next = indent + "| ";
                        }
                        build(string.Format("{0}{1}/", directory, children[i]), paths, next);
                  }
            }

            private List<string> result = new List<string>();
      }
}