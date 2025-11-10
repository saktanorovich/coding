using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class ParkingLot {
            public int fastest(string[] lot) {
                  return fastest(new Parking(lot, lot.Length, lot[0].Length));
            }

            private int fastest(Parking parking) {
                  List<ParkingSpot> availableParkingSpots = new List<ParkingSpot>();
                  foreach (ParkingSpot parkingSpot in parking) {
                        if (parking[parkingSpot] == 'A') {
                              availableParkingSpots.Add(parkingSpot);
                        }
                  }
                  List<ParkingSpotVisitorResult> carresults = new List<ParkingSpotVisitorResult>();
                  List<ParkingSpotVisitorResult> topresults = new List<ParkingSpotVisitorResult>();
                  int[,] manresults = null;
                  foreach (ParkingSpot parkingSpot in parking) {
                        char obj = parking[parkingSpot];
                        if ("ECY".IndexOf(obj) >= 0) {
                              ParkingSpotVisitor parkingSpotVisitor = new ParkingSpotVisitor(parking, parkingSpot);
                              int[,] distances = bfs(parking, parkingSpot, parkingSpotVisitor);
                              if ("CY".IndexOf(obj) >= 0) {
                                    foreach (ParkingSpot destination in availableParkingSpots) {
                                          ParkingSpotVisitorResult result = parkingSpotVisitor.getResult(destination, distances);
                                          if (result != null) {
                                                carresults.Add(result);
                                          }
                                    }
                              }
                              else manresults = distances;
                        }
                  }
                  carresults.Sort();
                  int[,] occupied = new int[parking.n, parking.m];
                  for (int xpos = 0; xpos < parking.n; ++xpos) {
                        for (int ypos = 0; ypos < parking.m; ++ypos) {
                              occupied[xpos, ypos] = -1;
                        }
                  }
                  List<int> assignedCars = new List<int>();
                  foreach (ParkingSpotVisitorResult result in carresults) {
                        ParkingSpot destination = result.destination;
                        if (parking[result.parkingSpotVisitor.original] == 'Y') {
                              if (occupied[destination.xpos, destination.ypos] == -1) {
                                    topresults.Add(result);
                              }
                        }
                        else if (!assignedCars.Contains(result.parkingSpotVisitor.id)) {
                              if (occupied[destination.xpos, destination.ypos] == -1) {
                                    assignedCars.Add(result.parkingSpotVisitor.id);
                                    occupied[destination.xpos, destination.ypos] = result.parkingSpotVisitor.id;
                              }
                        }
                  }
                  int res = int.MaxValue;
                  foreach (ParkingSpotVisitorResult car in topresults) {
                        if (car.destination[manresults] < int.MaxValue) {
                              res = Math.Min(res, car.distance + 2 * car.destination[manresults]);
                        }
                  }
                  if (res < int.MaxValue) {
                        return res;
                  }
                  return -1;
            }

            private int[,] bfs(Parking parking, ParkingSpot parkingSpot, ParkingSpotVisitor parkingSpotVisitor) {
                  int[,] distance = new int[parking.n, parking.m];
                  for (int x = 0; x < parking.n; ++x) {
                        for (int y = 0; y < parking.m; ++y) {
                              distance[x, y] = int.MaxValue;
                        }
                  }
                  Queue<ParkingSpot> queue = new Queue<ParkingSpot>();
                  for (parkingSpot[distance] = 0, queue.Enqueue(parkingSpot); queue.Count > 0; ) {
                        parkingSpot = queue.Dequeue();
                        foreach (ParkingSpot next in parkingSpotVisitor.next(parking, parkingSpot)) {
                              if (next[distance] > parkingSpot[distance] + 1) {
                                    next[distance] = parkingSpot[distance] + 1;
                                    queue.Enqueue(next);
                              }
                        }
                  }
                  return distance;
            }

            private class Parking : IEnumerable<ParkingSpot> {
                  public readonly string[] lot;
                  public readonly int n;
                  public readonly int m;

                  public Parking(string[] lot, int n, int m) {
                        this.lot = lot;
                        this.n = n;
                        this.m = m;
                  }

                  public char this[ParkingSpot parkingSpot] {
                        get {
                              if (0 <= parkingSpot.xpos && parkingSpot.xpos < n) {
                                    if (0 <= parkingSpot.ypos && parkingSpot.ypos < m) {
                                          return lot[parkingSpot.xpos][parkingSpot.ypos];
                                    }
                              }
                              return '?';
                        }
                  }

                  public IEnumerator<ParkingSpot> GetEnumerator() {
                        for (int x = 0; x < n; ++x) {
                              for (int y = 0; y < m; ++y) {
                                    yield return new ParkingSpot(x, y);
                              }
                        }
                  }

                  IEnumerator IEnumerable.GetEnumerator() {
                        return this.GetEnumerator();
                  }
            }

            private class ParkingSpot : IComparable<ParkingSpot> {
                  public readonly int xpos;
                  public readonly int ypos;

                  public ParkingSpot(int xpos, int ypos) {
                        this.xpos = xpos;
                        this.ypos = ypos;
                  }

                  public int this[int[,] distances] {
                        get { return distances[xpos, ypos]; }
                        set { distances[xpos, ypos] = value; }
                  }

                  public int CompareTo(ParkingSpot other) {
                        if (this.xpos != other.xpos) {
                              return this.xpos.CompareTo(other.xpos);
                        }
                        return this.ypos.CompareTo(other.ypos);
                  }
            }

            private class ParkingSpotVisitor {
                  public readonly ParkingSpot original;
                  public readonly int id;
                  public readonly string allowance;

                  public ParkingSpotVisitor(Parking parking, ParkingSpot original) {
                        this.original = original;
                        this.id = original.xpos * 100 + original.ypos;
                        switch (parking[original]) {
                              case 'C':
                              case 'Y':
                                    allowance = carallowance;
                                    break;
                              case 'E':
                                    allowance = manallowance;
                                    break;
                        }
                  }

                  public IEnumerable<ParkingSpot> next(Parking parking, ParkingSpot parkingSpot) {
                        List<ParkingSpot> result = new List<ParkingSpot>();
                        if (!complete(parking, parkingSpot)) {
                              for (int k = 0; k < 4; ++k) {
                                    int xpos = parkingSpot.xpos + dx[k];
                                    int ypos = parkingSpot.ypos + dy[k];
                                    ParkingSpot destination = new ParkingSpot(xpos, ypos);
                                    if (accept(parking, destination)) {
                                          result.Add(destination);
                                    }
                              }
                        }
                        return result;
                  }

                  public bool accept(Parking parking, ParkingSpot parkingSpot) {
                        if (allowance.IndexOf(parking[parkingSpot]) >= 0) {
                              return true;
                        }
                        return false;
                  }

                  public bool complete(Parking parking, ParkingSpot parkingSpot) {
                        if ("CY".IndexOf(parking[original]) >= 0) {
                              if (parking[parkingSpot] == 'A') {
                                    return true;
                              }
                        }
                        return false;
                  }

                  public ParkingSpotVisitorResult getResult(ParkingSpot destination, int[,] distance) {
                        if (destination[distance] < int.MaxValue) {
                              return new ParkingSpotVisitorResult(destination, this, destination[distance]);
                        }
                        return null;
                  }

                  private static readonly int[] dx = new int[4] { -1, 0, +1, 0 };
                  private static readonly int[] dy = new int[4] { 0, -1, 0, +1 };

                  private static readonly string carallowance = ".ACY";
                  private static readonly string manallowance = ".ACYUE";
            }

            private class ParkingSpotVisitorResult : IComparable<ParkingSpotVisitorResult> {
                  public readonly ParkingSpot destination;
                  public readonly ParkingSpotVisitor parkingSpotVisitor;
                  public readonly int distance;

                  public ParkingSpotVisitorResult(ParkingSpot destination, ParkingSpotVisitor parkingSpotVisitor, int distance) {
                        this.destination = destination;
                        this.parkingSpotVisitor = parkingSpotVisitor;
                        this.distance = distance;
                  }

                  public int CompareTo(ParkingSpotVisitorResult other) {
                        if (this.distance.CompareTo(other.distance) != 0) {
                              return this.distance.CompareTo(other.distance);
                        }
                        if (this.destination.CompareTo(other.destination) != 0) {
                              return this.destination.CompareTo(other.destination);
                        }
                        return this.parkingSpotVisitor.id.CompareTo(other.parkingSpotVisitor.id);
                  }
            }
      }
}