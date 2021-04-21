import java.math.*;

      public class DigitsSwap {
            /* it is can be easily shown that the product is maximal in the case when x equals to y... */
            public String maximalProduct(String x, String y, int swaps) {
                  return getMaximalProduct(x, y, swaps).toString();
            }

            private static BigInteger getMaximalProduct(String x, String y, int swaps) {
                  BigInteger a = getMaximalProduct(x.toCharArray(), y.toCharArray(), swaps);
                  BigInteger b = getMaximalProduct(y.toCharArray(), x.toCharArray(), swaps);
                  return a.compareTo(b) > 0 ? a : b;
            }

            private static BigInteger getMaximalProduct(char[] x, char[] y, int swaps) {
                  if (swaps > 0) {
                        boolean hasTheSameDigit = false;
                        for (int i = 0; i < x.length; ++i) {
                              if (x[i] != y[i]) {
                                    if (x[i] < y[i]) {
                                          char temp = x[i]; x[i] = y[i]; y[i] = temp;
                                          swaps = swaps - 1;
                                    }
                                    for (int j = i + 1; j < x.length; ++j) {
                                          if (x[j] > y[j] && swaps > 0) {
                                                char temp = x[j]; x[j] = y[j]; y[j] = temp;
                                                swaps = swaps - 1;
                                                continue;
                                          }
                                          hasTheSameDigit |= (x[j] == y[j]);
                                    }
                                    if (swaps % 2 == 1 && !hasTheSameDigit) {
                                          char temp = x[x.length - 1];
                                          x[x.length - 1] = y[x.length - 1];
                                          y[x.length - 1] = temp;
                                    }
                                    break; 
                              }
                              hasTheSameDigit = true;
                        }
                  }
                  return new BigInteger(new String(x)).multiply(new BigInteger(new String(y)));
            }

            public static void main(String args[]) {
                  System.out.println(new DigitsSwap().maximalProduct("45", "13", 1).equals("645"));
                  System.out.println(new DigitsSwap().maximalProduct("321", "123", 2).equals("39483"));
                  System.out.println(new DigitsSwap().maximalProduct("4531", "1332", 0).equals("6035292"));
                  System.out.println(new DigitsSwap().maximalProduct("13425", "87694", 99).equals("1476187680"));
                  System.out.println(new DigitsSwap().maximalProduct("2872876342876443222", "2309482482304823423", 5).equals("6669566046086333877050194232995188906"));
                  System.out.println(new DigitsSwap().maximalProduct("940948", "124551", 4893846).equals("133434353148"));
                  System.out.println(new DigitsSwap().maximalProduct("56710852", "18058360", 1).equals("1050671725722720"));
                  System.out.println(new DigitsSwap().maximalProduct("1", "9", 1000000000).equals("9"));
            }
      }