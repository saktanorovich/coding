using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class MatArith {
            public string[] calculate(string[] a, string[] b, string[] c, string equation) {
                  long[][] result = calculate(parse(a), parse(b), parse(c), new ShuntingYardAlgorithm().GetPostfixForm(reverse(equation)));
                  if (result != null) {
                        return Array.ConvertAll(result, delegate(long[] r) {
                              string res = string.Empty;
                              for (int i = 0; i < r.Length; ++i) {
                                    res += r[i].ToString();
                                    if (i + 1 < r.Length) {
                                          res += ' ';
                                    }
                              }
                              return res;
                        });
                  }
                  return new string[0];
            }

            private string reverse(string equation) {
                  string result = string.Empty;
                  for (int i = 0; i < equation.Length; ++i) {
                        result += equation[equation.Length - 1 - i];
                  }
                  return result;
            }

            private long[][] calculate(long[][] a, long[][] b, long[][] c, string equation) {
                  Stack<long[][]> tokens = new Stack<long[][]>();
                  for (int i = 0; i < equation.Length; ++i) {
                        if (IsLetter(equation[i])) {
                              switch (equation[i]) {
                                    case 'A': tokens.Push(a); break;
                                    case 'B': tokens.Push(b); break;
                                    case 'C': tokens.Push(c); break;
                              }
                        }
                        else if (IsOperator(equation[i])) {
                              long[][] x = tokens.Pop();
                              long[][] y = tokens.Pop();
                              long[][] z = null;
                              switch (equation[i]) {
                                    case '*': z = mul(x, y); break;
                                    case '+': z = sum(x, y); break;
                                    case '-': z = sub(x, y); break;
                              }
                              if (z != null) {
                                    tokens.Push(z);
                              }
                              else return null;
                        }
                        else {
                              throw new Exception();
                        }
                  }
                  return tokens.Peek();
            }

            private long[][] sum(long[][] a, long[][] b) {
                  if (a.Length == b.Length && a[0].Length == b[0].Length) {
                        long[][] result = new long[a.Length][];
                        for (int i = 0; i < a.Length; ++i) {
                              result[i] = new long[a[0].Length];
                              for (int j = 0; j < a[0].Length; ++j) {
                                    result[i][j] = a[i][j] + b[i][j];
                                    if (int.MinValue > result[i][j] || result[i][j] > int.MaxValue) {
                                          return null;
                                    }
                              }
                        }
                        return result;
                  }
                  return null;
            }

            private long[][] sub(long[][] a, long[][] b) {
                  if (a.Length == b.Length && a[0].Length == b[0].Length) {
                        long[][] result = new long[a.Length][];
                        for (int i = 0; i < a.Length; ++i) {
                              result[i] = new long[a[0].Length];
                              for (int j = 0; j < a[0].Length; ++j) {
                                    result[i][j] = a[i][j] - b[i][j];
                                    if (int.MinValue > result[i][j] || result[i][j] > int.MaxValue) {
                                          return null;
                                    }
                              }
                        }
                        return result;
                  }
                  return null;
            }

            private long[][] mul(long[][] a, long[][] b) {
                  if (a[0].Length == b.Length) {
                        long[][] result = new long[a.Length][];
                        for (int i = 0; i < a.Length; ++i) {
                              result[i] = new long[b[0].Length];
                        }
                        for (int i = 0; i < a.Length; ++i) {
                              for (int j = 0; j < b[0].Length; ++j) {
                                    for (int k = 0; k < a[0].Length; ++k) {
                                          result[i][j] += a[i][k] * b[k][j];
                                    }
                                    if (int.MinValue > result[i][j] || result[i][j] > int.MaxValue) {
                                          return null;
                                    }
                              }
                        }
                        return result;
                  }
                  return null;
            }

            private long[][] parse(string[] a) {
                  return Array.ConvertAll(a, delegate(string s) {
                        return Array.ConvertAll(s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                              delegate(string x) {
                                    return long.Parse(x);
                              });
                  });
            }

            private static bool IsLetter(char token) {
                  return ('A' <= token && token <= 'Z');
            }

            private static bool IsOperator(char token) {
                  switch (token) {
                        case '*':
                        case '+':
                        case '-':
                              return true;
                  }
                  return false;
            }

            private static bool IsBrace(char token) {
                  switch (token) {
                        case '(':
                        case ')':
                              return true;
                  }
                  return false;
            }

            private static int Priority(char token) {
                  switch (token) {
                        case '*':
                              return 2;
                        case '+':
                        case '-':
                              return 1;
                  }
                  return 0;
            }

            private class ShuntingYardAlgorithm {
                  private string postfixForm;
                  private Stack<char> operatorsStack = new Stack<char>();

                  private void AppendToken(char token) {
                        postfixForm += token;
                  }

                  private void HandleOperatorToken(char token) {
                        while (operatorsStack.Count > 0) {
                              char top = operatorsStack.Peek();
                              if (Priority(top) > Priority(token)) {
                                    operatorsStack.Pop();
                                    AppendToken(top);
                              }
                              else break;
                        }
                        operatorsStack.Push(token);
                  }

                  private void HandleAlphabetToken(char token) {
                        if (IsBrace(token)) {
                              operatorsStack.Push(token);
                              if (token == ')') {
                                    operatorsStack.Pop();
                                    do {
                                          token = operatorsStack.Peek();
                                          operatorsStack.Pop();
                                          if (token == '(') {
                                                break;
                                          }
                                          AppendToken(token);
                                    } while (true);
                              }
                        }
                        else {
                              AppendToken(token);
                        }
                  }

                  public string GetPostfixForm(string expression) {
                        postfixForm = string.Empty;
                        for (int i = 0; i < expression.Length; ++i) {
                              char token = expression[i];
                              if (IsOperator(token)) {
                                    HandleOperatorToken(token);
                              }
                              else {
                                    HandleAlphabetToken(token);
                              }
                        }
                        while (operatorsStack.Count > 0) {
                              AppendToken(operatorsStack.Peek());
                              operatorsStack.Pop();
                        }
                        return postfixForm;
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
                  Console.WriteLine(ToString(new MatArith().calculate(
                        new string[] { "1 2 3", "2 3 4" },
                        new string[] { "1 2", "3 4", "5 6" },
                        new string[] { "1" }, "A*B")));
                  Console.WriteLine(ToString(new MatArith().calculate(
                        new string[] { "8" },
                        new string[] { "2" },
                        new string[] { "-1" }, "A*A*A*A*A*A*A*A*A*A*B*C")));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}