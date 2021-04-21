import java.io.*;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        PrintWriter out = new PrintWriter(System.out);
        for (int testCase = 1; true; ++testCase) {
            int numOfStations = in.nextInt();
            if (numOfStations == 0) {
                break;
            }
            int appointmentTime = in.nextInt();
            int[] time = new int[numOfStations];
            for (int i = 0; i < numOfStations - 1; ++i) {
                time[i] = in.nextInt();
            }
            boolean[][][] schedule = new boolean[2][appointmentTime + 1][numOfStations];
            for (int i = 0; i < 2; ++i) {
                int numOfTrains = in.nextInt();
                int stp = 1 - 2 * i;
                int beg = (numOfStations - 1) * (i);
                int end = (numOfStations - 1) * (1 - i);
                for (int t = 0; t < numOfTrains; ++t) {
                    int at = in.nextInt();
                    for (int station = beg; true; station += stp) {
                        if (at <= appointmentTime) {
                            schedule[i][at][station] = true;
                        }
                        if (station != end) {
                            at += time[station - i];
                        }
                        else {
                            break;
                        }
                    }
                }
            }
            int[][] best = new int[appointmentTime + 1][numOfStations];
            for (int at = 0; at <= appointmentTime; ++at) {
                Arrays.fill(best[at], Integer.MAX_VALUE);
            }
            best[0][0] = 0;
            for (int at = 0; at < appointmentTime; ++at) {
                for (int station = 0; station < numOfStations; ++station) {
                    if (best[at][station] < Integer.MAX_VALUE) {
                        if (best[at + 1][station] > best[at][station] + 1) {
                            best[at + 1][station] = best[at][station] + 1;
                        }
                        if (schedule[0][at][station] && station < numOfStations - 1) {
                            if (at + time[station] <= appointmentTime) {
                                if (best[at + time[station]][station + 1] > best[at][station]) {
                                    best[at + time[station]][station + 1] = best[at][station];
                                }
                            }
                        }
                        if (schedule[1][at][station] && station > 0) {
                            if (at + time[station - 1] <= appointmentTime) {
                                if (best[at + time[station - 1]][station - 1] > best[at][station]) {
                                    best[at + time[station - 1]][station - 1] = best[at][station];
                                }
                            }
                        }
                    }
                }
            }
            out.printf("Case Number %d: ", testCase);
            int waitingTime = best[appointmentTime][numOfStations - 1];
            if (waitingTime < Integer.MAX_VALUE)
                out.println(waitingTime);
            else
                out.println("impossible");
        }
        in.close();
        out.close();
    }
}
