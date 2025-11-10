using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Whisper {
            public string toWhom(string[] usernames, string typed) {
                  string result = toWhomInternal(Array.ConvertAll(usernames, delegate(string s) {
                        return s.ToLower();
                  }), typed.ToLower() + '?');
                  if (result != err && result != not) {
                        foreach (string user in usernames) {
                              if (result.Equals(user.ToLower())) {
                                    result = user;
                                    break;
                              }
                        }
                  }
                  return result;
            }

            private readonly string msg = "/msg ";
            private readonly string err = "not a whisper";
            private readonly string not = "user is not logged in";

            private string toWhomInternal(string[] users, string typed) {
                  for (int i = 0; i < msg.Length; ++i) {
                        if (typed[i] != msg[i]) {
                              return err;
                        }
                  }
                  if (typed[msg.Length] != ' ') {
                        typed = typed.Substring(msg.Length, typed.Length - msg.Length);
                        string result = string.Empty;
                        foreach (string user in users) {
                              if (typed.IndexOf(user) == 0 && typed[user.Length] == ' ') {
                                    if (string.IsNullOrEmpty(result) || result.Length < user.Length) {
                                          result = user;
                                    }
                              }
                        }
                        if (string.IsNullOrEmpty(result)) {
                              result = not;
                        }
                        return result;
                  }
                  return not;
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
                  //Console.WriteLine(new Whisper().toWhom(
                  //      new string[] { "John", "John Doe", "John Doe h" },
                  //      "/msg John Doe hi there"));
                  Console.WriteLine(new Whisper().toWhom(
                        new string[] { "me" },
                        "/msg  me hi"));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}