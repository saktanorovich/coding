using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class QuiningTopCoder {
            public string testCode(string source) {
                  Stack<int> stack = new Stack<int>();
                  string output = string.Empty;
                  for (int it = 0, ip = 0, dt = 1; it < maxNumOfCycles; ++it) {
                        try {
                              int dc = dt;
                              switch (source[ip]) {
                                    case '0':
                                    case '1':
                                    case '2':
                                    case '3':
                                    case '4':
                                    case '5':
                                    case '6':
                                    case '7':
                                    case '8':
                                    case '9':
                                          push(stack, source[ip] - '0');
                                          break;
                                    case '$':
                                          pop(stack);
                                          break;
                                    case ':':
                                          int value = pop(stack);
                                          push(stack, value);
                                          push(stack, value);
                                          break;
                                    case 'W':
                                          int a = pop(stack);
                                          int b = pop(stack);
                                          push(stack, a);
                                          push(stack, b);
                                          break;
                                    case ',':
                                          output += source[Math.Abs(pop(stack)) % source.Length];
                                          break;
                                    case '+': {
                                          push(stack, pop(stack) + pop(stack));
                                          break;
                                    }
                                    case '-':
                                          push(stack, pop(stack) - pop(stack));
                                          break;
                                    case '#':
                                          dc = +2 * dc;
                                          break;
                                    case 'R':
                                          dt = -1 * dt;
                                          dc = -1 * dc;
                                          break;
                                    case 'S':
                                          int s = pop(stack);
                                          push(stack, s > 0 ? +1 : -1);
                                          break;
                                    case '_':
                                          dt = dc = pop(stack) % source.Length;
                                          break;
                                    case 'J':
                                          ip = Math.Abs(pop(stack)) % source.Length;
                                          goto next;
                                    case '@':
                                          throw new Exception("BADEND");
                              }
                              ip = (3 * source.Length + ip + dc) % source.Length;
                              next: {
                                    match(source, output);
                              }
                        }
                        catch (Exception ex) {
                              return string.Format("{0} {1}", ex.Message, it);
                        }
                  }
                  return "TIMEOUT";
            }

            private void match(string source, string output) {
                  if (source.Equals(output)) {
                        throw new Exception("QUINES");
                  }
                  else if (source.StartsWith(output)) {
                  }
                  else {
                        throw new Exception("MISMATCH");
                  }
            }

            private void push(Stack<int> stack, int x) {
                  if (-oo <= x && x <= +oo) {
                        stack.Push(x);
                        return;
                  }
                  throw new Exception("OVERFLOW");
            }

            private int pop(Stack<int> stack) {
                  if (stack.Count > 0) {
                        return stack.Pop();
                  }
                  return 0;
            }

            private static readonly int maxNumOfCycles = 80000;
            private static readonly int oo = +1000000000;
      }
}