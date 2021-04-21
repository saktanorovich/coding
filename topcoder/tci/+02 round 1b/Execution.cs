using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TopCoder.Algorithms {
      public class Execution {
            public long analyze(string[] code) {
                  string program = string.Empty;
                  for (int i = 0; i < code.Length; ++i) {
                        program = program + code[i].Replace(" ", string.Empty);
                  }
                  return new Node(string.Format("for(1){{{0}}}", program)).GetCost();
            }

            private class Node {
                  private static readonly Regex forstatement = new Regex(@"^for\(([0-9]+)\){.*}$");
                  private static readonly Regex basstatement = new Regex(@"^BASIC;$");
                  private static readonly Regex intstatement = new Regex(@"[0-9]+");
                  private static readonly Regex lopstatement = new Regex(@"{.*}");

                  private static readonly string forstring = "for(";
                  private static readonly string basstring = "BASIC;";

                  private long cost;
                  private List<Node> inner;

                  public Node(string code) {
                        if (!string.IsNullOrEmpty(code)) {
                              if (basstatement.IsMatch(code)) {
                                    cost = 1;
                              }
                              else if (forstatement.IsMatch(code)) {
                                    cost = long.Parse(intstatement.Match(code).ToString());
                                    ProcessLoop(lopstatement.Match(code).ToString());
                              }
                        }
                  }

                  public void ProcessLoop(string code) {
                        code = code.Substring(1, code.Length - 2);
                        inner = new List<Node>();
                        while (!string.IsNullOrEmpty(code)) {
                              if (code.StartsWith(basstring)) {
                                    inner.Add(new Node(code.Substring(0, basstring.Length)));
                                    code = code.Substring(basstring.Length, code.Length - basstring.Length);
                              }
                              else if (code.StartsWith(forstring)) {
                                    for (int i = 0, loops = 0; i < code.Length; ++i) {
                                          if (code[i] == '{') {
                                                loops = loops + 1;
                                          }
                                          if (code[i] == '}') {
                                                loops = loops - 1;
                                                if (loops == 0) {
                                                      inner.Add(new Node(code.Substring(0, i + 1)));
                                                      code = code.Substring(i + 1, code.Length - i - 1);
                                                      break;
                                                }
                                          }
                                    }
                              }
                        }
                  }

                  public long GetCost() {
                        if (inner != null) {
                              long total = 0;
                              foreach (Node node in inner) {
                                    total += node.GetCost();
                              }
                              return cost * total;
                        }
                        return cost;
                  }
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new Execution().analyze(new string[] {"BASIC;","for(10){","  for(100){}","  BASIC;","  for(20){","    BASIC;","    BASIC;","  }","  BASIC;" ,"}"}));
                  Console.WriteLine(new Execution().analyze(new string[] {"BASIC;for(10){BASIC;}"}));
                  Console.WriteLine(new Execution().analyze(new string[] {"for(0){}"}));
                  Console.WriteLine(new Execution().analyze(new string[] { "for(10900){}BASIC;" }));
                  Console.WriteLine(new Execution().analyze(new string[] {"for(2){for(2){for(2){for(2){for(2){for(2){for(2){"
                        ,"for(2){for(2){for(2){for(2){for(2){for(2){for(2){","for(2){for(2){for(2){for(2){for(2){for(2){for(2){","for(2){for(2){for(2){for(2){for(2){for(2){for(2){"
                        ,"for(2){for(2){for(2){for(2){for(2){for(2){for(2){","for(2){for(2){for(2){for(2){for(2){for(2){for(2){","for(2){for(2){for(2){for(2){for(2){for(2){for(2){"
                        ,"for(2){for(2){for(2){for(2){for(2){for(2){for(2){","BASIC;","}}}}}}}}}}}}}}}}}}}}}}}}}}}}","}}}}}}}}}}}}}}}}}}}}}}}}}}}}"}));
                  Console.WriteLine(new Execution().analyze(new string[] {"for(0){}","for(1)","{BASIC;","for(5){BASIC;}","for(2){BASIC;}","BASIC;","  for","( 3 ){BASIC;}}"}));
                  Console.WriteLine(new Execution().analyze(new string[] {"BASIC;for(7){for(3){}for(0){BASIC;}}"}));
                  Console.WriteLine(new Execution().analyze(new string[] {"for(9223372036854775807){BASIC;}"}));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}