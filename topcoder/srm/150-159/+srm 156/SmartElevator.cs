using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class SmartElevator {
            public int timeWaiting(int[] arrivalTime, int[] startingFloor, int[] destinationFloor) {
                  Passenger[] passengers = new Passenger[arrivalTime.Length];
                  for (int i = 0; i < passengers.Length; ++i) {
                        passengers[i] = new Passenger(arrivalTime[i], startingFloor[i], destinationFloor[i]);
                  }
                  return timeWaiting(new ElevatorState((1 << passengers.Length) - 1, (1 << passengers.Length) - 1, 0, 1), passengers);
            }

            private int timeWaiting(ElevatorState state, Passenger[] passengers) {
                  if (state.completed()) {
                        return state.time;
                  }
                  else {
                        int result = int.MaxValue;
                        foreach (ElevatorState next in state.getNext(passengers)) {
                              result = Math.Min(result, timeWaiting(next, passengers));
                        }
                        return result;
                  }
            }

            private class ElevatorState {
                  public int time;
                  public int flor;
                  public int incoming;
                  public int outgoing;

                  public ElevatorState(int incoming, int outgoing, int time, int flor) {
                        this.time = time;
                        this.flor = flor;
                        this.incoming = incoming;
                        this.outgoing = outgoing;
                  }

                  public bool completed() {
                        return incoming + outgoing == 0;
                  }

                  public List<ElevatorState> getNext(Passenger[] passengers) {
                        List<ElevatorState> result = new List<ElevatorState>();
                        for (int who = 0; who < passengers.Length; ++who) {
                              if ((incoming & (1 << who)) != 0) {
                                    result.Add(new ElevatorState(incoming ^ (1 << who), outgoing, passengers[who].incomingTime(time, flor), passengers[who].srcfloor));
                                    continue;
                              }
                              if ((outgoing & (1 << who)) != 0) {
                                    result.Add(new ElevatorState(incoming, outgoing ^ (1 << who), passengers[who].outgoingTime(time, flor), passengers[who].dstfloor));
                                    continue;
                              }
                        }
                        return result;
                  }
            }

            private class Passenger {
                  public int arrivalt;
                  public int srcfloor;
                  public int dstfloor;

                  public Passenger(int arrivalt, int srcfloor, int dstfloor) {
                        this.arrivalt = arrivalt;
                        this.srcfloor = srcfloor;
                        this.dstfloor = dstfloor;
                  }

                  public int incomingTime(int time, int floor) {
                        return Math.Max(arrivalt, time + Math.Abs(srcfloor - floor));
                  }

                  public int outgoingTime(int time, int floor) {
                        return time + Math.Abs(dstfloor - floor);
                  }
            }
      }
}