using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class LongStraightRoad {
            public int distanceToDestination(string[] signs, string destination) {
                  return distanceToDestination(Sign.GetSigns(signs), Place.GetPlaceByName(destination));
            }

            private int distanceToDestination(Sign[] signs, Place destination) {
                  return distanceToDestination(signs, signs[signs.Length - 1], destination, signs.Length, Place.GetNumOfPlaces());
            }

            private const double oo  = 1e+10;
            private const double eps = 1e-10;

            private int distanceToDestination(Sign[] signs, Sign origin, Place destination, int numOfSigns, int numOfPlaces) {
                  int numOfEntities = numOfSigns + numOfPlaces;
                  double[,] distance = new double[numOfEntities, numOfEntities];
                  for (int i = 0; i < numOfEntities; ++i) {
                        for (int j = i + 1; j < numOfEntities; ++j) {
                              distance[i, j] = +oo;
                              distance[j, i] = +oo;
                        }
                  }
                  foreach (Sign sign in signs) {
                        foreach (SignItem signItem in sign.SignItems) {
                              int placeIdInGraph = signItem.Place.Id + numOfSigns;
                              distance[sign.Id, placeIdInGraph] = +signItem.Distance;
                              distance[placeIdInGraph, sign.Id] = -signItem.Distance;
                        }
                  }
                  for (int iSign = numOfSigns - 1; iSign >= 0; --iSign) {
                        foreach (SignItem iSignItem in signs[iSign].SignItems) {
                              for (int jSign = iSign - 1; jSign >= 0; --jSign) {
                                    foreach (SignItem jSignItem in signs[jSign].SignItems) {
                                          if (iSignItem.Place.Id == jSignItem.Place.Id) {
                                                if (jSignItem.Distance > iSignItem.Distance) {
                                                      int d = jSignItem.Distance - iSignItem.Distance;
                                                      if (distance[jSign, iSign] == +oo) {
                                                            distance[jSign, iSign] = +d;
                                                            distance[iSign, jSign] = -d;
                                                      }
                                                      if (distance[jSign, iSign] != d) {
                                                            return -1;
                                                      }
                                                }
                                                else return -1;
                                          }
                                    }
                              }
                        }
                  }
                  List<Edge>[] graph = Array.ConvertAll(new object[numOfSigns + 1],
                        delegate(object x) {
                              return new List<Edge>();
                  });
                  for (int iSign = 1; iSign + 1 <= numOfSigns; ++iSign) {
                        graph[iSign + 1].Add(new Edge(iSign + 1, iSign, -eps));
                  }
                  for (int iSign = 1; iSign <= numOfSigns; ++iSign) {
                        for (int jSign = 1; jSign <= numOfSigns; ++jSign) {
                              if (distance[iSign - 1, jSign - 1] != +oo) {
                                    graph[iSign].Add(new Edge(iSign, jSign, distance[iSign - 1, jSign - 1]));
                              }
                        }
                  }
                  for (int iSign = 1; iSign <= numOfSigns; ++iSign) {
                        graph[0].Add(new Edge(0, iSign, 0));
                  }
                  /* perform bellman-ford algorithm in order to check whether the system of difference constraints has a feasible solution... */
                  double[] diffDistance = new double[numOfSigns + 1];
                  for (int iSign = 1; iSign <= numOfSigns; ++iSign) {
                        diffDistance[iSign] = +oo;
                  }
                  for (int i = 0; i < numOfSigns; ++i) {
                        for (int sign = 0; sign <= numOfSigns; ++sign) {
                              foreach (Edge e in graph[sign]) {
                                    if (diffDistance[e.DstSign] > diffDistance[e.SrcSign] + e.Length) {
                                          diffDistance[e.DstSign] = diffDistance[e.SrcSign] + e.Length;
                                    }
                              }
                        }
                  }
                  for (int sign = 0; sign <= numOfSigns; ++sign) {
                        foreach (Edge e in graph[sign]) {
                              if (diffDistance[e.DstSign] > diffDistance[e.SrcSign] + e.Length) {
                                    return -1;
                              }
                        }
                  }
                  /* at this moment the signs are valid so try to find distances to the places... */
                  for (bool inprogress = true; inprogress; ) {
                        inprogress = false;
                        for (int iSign = 0; iSign < numOfSigns; ++iSign) {
                              for (int place = 0; place < numOfPlaces; ++place) {
                                    int placeIdInGraph = place + numOfSigns;
                                    if (distance[iSign, placeIdInGraph] == +oo) {
                                          for (int jSign = iSign + 1; jSign < numOfSigns; ++jSign) {
                                                if (distance[iSign, jSign] != +oo) {
                                                      if (distance[jSign, placeIdInGraph] != +oo) {
                                                            distance[iSign, placeIdInGraph] = +(distance[jSign, placeIdInGraph] + distance[iSign, jSign]);
                                                            distance[placeIdInGraph, iSign] = -(distance[jSign, placeIdInGraph] + distance[iSign, jSign]);
                                                            inprogress = true;
                                                      }
                                                }
                                          }
                                          for (int jSign = 0; jSign < iSign; ++jSign) {
                                                if (distance[jSign, iSign] != +oo) {
                                                      if (distance[jSign, placeIdInGraph] != +oo) {
                                                            distance[iSign, placeIdInGraph] = +(distance[jSign, placeIdInGraph] - distance[jSign, iSign]);
                                                            distance[placeIdInGraph, iSign] = -(distance[jSign, placeIdInGraph] - distance[jSign, iSign]);
                                                            inprogress = true;
                                                      }
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  if (distance[origin.Id, numOfSigns + destination.Id] >= 0 &&
                        distance[origin.Id, numOfSigns + destination.Id] != +oo) {
                              return (int)distance[origin.Id, numOfSigns + destination.Id];
                  }
                  return -1;
            }

            private class Edge {
                  public int SrcSign { get; private set; }
                  public int DstSign { get; private set; }
                  public double Length { get; private set; }

                  public Edge(int srcSign, int dstSign, double length) {
                        SrcSign = srcSign;
                        DstSign = dstSign;
                        Length  = length;
                  }
            }

            private class Place {
                  private static Dictionary<string, Place> placeByName = new Dictionary<string, Place>();
                  private static int numOfPlaces = 0;

                  public static Place GetPlaceByName(string name) {
                        if (!placeByName.ContainsKey(name)) {
                              placeByName[name] = new Place(numOfPlaces, name);
                              numOfPlaces = numOfPlaces + 1;
                        }
                        return placeByName[name];
                  }

                  public static int GetNumOfPlaces() {
                        return numOfPlaces;
                  }

                  private Place(int id, string name) {
                        Id = id;
                        Name = name;
                  }

                  public int Id { get; private set; }
                  public string Name { get; private set; }
            }

            private class Sign {
                  public static Sign[] GetSigns(string[] signs) {
                        Sign[] result = new Sign[signs.Length];
                        for (int i = 0; i < signs.Length; ++i) {
                              result[i] = new Sign(i, Sign.Parse(signs[i]));
                        }
                        return result;
                  }

                  private static SignItem[] Parse(string sign) {
                        return Array.ConvertAll<string, SignItem>(sign.Split(new char[] { ';' }),
                              delegate(string s) {
                                    return SignItem.Parse(s);
                              }
                        );
                  }

                  public int Id { get; private set; }
                  public SignItem[] SignItems { get; private set; }

                  private Sign(int id, SignItem[] signItems) {
                        Id = id;
                        SignItems = signItems;
                  }
            }

            private class SignItem {
                  public static SignItem Parse(string signItem) {
                        string[] splitted = signItem.Split(new char[] { ' ' });
                        return new SignItem(
                                    Place.GetPlaceByName(splitted[0]),
                                          int.Parse(splitted[1]));
                  }

                  public Place Place { get; private set; }
                  public int Distance { get; private set; }

                  private SignItem(Place place, int distance) {
                        Place = place;
                        Distance = distance;
                  }
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "A 1000",
                        "C 1000",
                        "C 750",
                        "D 1000",
                        "B 1000",
                        "A 500",
                        "D 250",
                        "B 500"}, "B")); // -1
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "COLCHESTER 5;GLASTONBURY 25;MARLBOROUGH 13",
                        "MARLBOROUGH 2"}, "GLASTONBURY")); // 14
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "COLCHESTER 5;GLASTONBURY 25;MARLBOROUGH 13",
                        "GLASTONBURY 13;MARLBOROUGH 2" }, "GLASTONBURY")); // -1
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "A 25;B 15",
                        "A 2" }, "B")); // -1
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "YVO 60;J 62",
                        "K 45",
                        "K 40;MV 17",
                        "K 37;YVO 44;HY 48;CC 69;D 77;YXF 80",
                        "YVO 30;B 37;RB 59" },"MV")); // 0
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "A 200;B 150",
                        "C 45;D 100;E 150",
                        "C 25;E 130",
                        "F 80;G 65",
                        "G 35;H 160",
                        "A 160",
                        "H 130" }, "F")); // -1
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] {
                        "A 10",
                        "B 10",
                        "B 9",
                        "C 10",
                        "C 9",
                        "A 7" }, "A")); // 7
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] { "HI 102;CAX 132;FP 179", "TM 199", "FL 97;DOL 119;GK 148",
                        "R 147", "QE 108", "FU 127;I 152", "R 143", "FL 89;DOL 111;B 136;GK 140", "ZP 144", "QE 101;DOL 107;CM 108;ZBI 143;RUB 144", "QE 99;CM 106;B 130",
                        "CUP 145", "LK 118;ZBI 130", "FL 71;RUB 130", "PWZ 161", "IW 75;H 80;ZBO 105;Z 113", "S 99;R 117;E 119", "IW 70", "TKB 91;PWZ 153;PN 160",
                        "VER 74;RD 130", "TKB 87;NO 137;PN 156", "VNG 73", "CZ 53;FU 86;ZP 106;RD 120;P 144", "FU 82;WO 110", "R 98", "H 55;Z 88", "TKB 67",
                        "CZ 40;LD 105", "FL 35;QE 51", "GHY 54", "WO 97;F 119", "CUP 98", "IW 37;NT 62", "MBX 32;LD 96;RD 98;F 114", "FP 104", "DOL 46;RUB 83;QFX 106",
                        "UP 38", "CM 42;RUB 78;PKZ 112", "W 72", "VNG 33;GHY 36;CAX 47;FP 94;TM 115", "CUP 82", "Y 10;JSP 61", "FU 46", "QE 25", "FL 8;V 88",
                        "PJW 17;UP 21;TKB 35;KQ 40", "B 51", "IW 10;HZT 75", "CAX 33;FP 80", "TKB 30;QZW 42;W 55" }, "PJW")); // 12
                  Console.WriteLine(new LongStraightRoad().distanceToDestination(new string[] { "BAD 135", "EZ 127;KWN 135;YL 163;BJ 193",
                        "UDP 110;YKX 122;BAD 131;T 177;MKY 191", "TGO 118;UAN 137", "KWN 132;SCI 167", "QS 170;A 190", "XB 93;GOF 110;W 116", "CU 96;TGO 111",
                        "QJ 105;TIT 128", "EZ 115;SEP 155", "TIT 125", "UAN 123", "VP 156", "PA 146;P 153", "TIT 116;MRA 174", "T 155", "YL 137", "KA 139",
                        "UAN 109;MPA 146", "MKY 161", "KWN 103", "A 157", "A 154", "YL 120", "TZ 65;ZCT 103;DOY 108;YL 119;SCI 126", "S 85;QT 121", "PLI 93;XE 104",
                        "P 121", "P 120", "EZ 74", "FFC 56;MA 87;TJG 89", "AX 47;UDP 56", "QT 108", "B 36;KG 43", "B 35", "EZ 60;SCI 103", "U 31;AX 34;AWI 63;KA 98;JIJ 114",
                        "DOY 83;SCI 101", "U 27;CH 41;UAN 67;KA 94;MKY 120", "PLI 62;BJ 113", "XB 20", "FK 72", "TJG 57;YM 65", "TGO 32;AWI 43;M 80",
                        "FFC 21;YM 62;FK 65;A 106", "UDP 20;MPA 85", "CU 13;YKX 31;M 76", "B 6;GZ 12;KG 13;X 63;MQH 68;VP 80",
                        "AWI 35;T 82", "P 75" }, "PA")); // 68

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}