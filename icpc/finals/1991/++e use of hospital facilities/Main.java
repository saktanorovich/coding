import java.io.*;
import java.util.*;

public class Main {
    private static final class UseOfHospitalFacilities {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            int numRoom = in.nextInt();
            int numBeds = in.nextInt();
            int startAt = in.nextInt() * 60;
            int timeRoom2Bed = in.nextInt();
            int timePrepRoom = in.nextInt();
            int timePrepBeds = in.nextInt();
            int numPatients = in.nextInt();
            Patient[] patients = new Patient[numPatients];
            for (int indx = 0; indx < numPatients; ++indx) {
                String name = in.next();
                int timeInRoom = in.nextInt();
                int timeInBeds = in.nextInt();
                patients[indx] = new Patient(indx, name, timeInRoom, timeInBeds);
            }
            Facility room[] = Facility.create(numRoom);
            Facility beds[] = Facility.create(numBeds);
            for (int p = 0, time = startAt; time <= 24 * 60; ++time) {
                List<Integer> transfer = new ArrayList<>();
                for (int i = 0; i < numRoom; ++i) {
                    if (room[i].available == 1 && room[i].readyTime <= time) {
                        room[i].available = 0;
                        if (room[i].patientId >= 0) {
                            transfer.add(room[i].patientId);
                            room[i].available = +1;
                            room[i].patientId = -1;
                            room[i].readyTime = time + timePrepRoom;
                        }
                    }
                }
                for (int i = 0; i < numBeds; ++i) {
                    if (beds[i].available == 1 && beds[i].readyTime <= time) {
                        beds[i].available = 0;
                        if (beds[i].patientId >= 0) {
                            beds[i].available = +1;
                            beds[i].patientId = -1;
                            beds[i].readyTime = time + timePrepBeds;
                        }
                    }
                }
                for (int x : transfer) {
                    for (int i = 0; i < numBeds; ++i) {
                        if (beds[i].available == 0) {
                            beds[i].available = 1;
                            beds[i].patientId = x;
                            beds[i].readyTime = patients[x].timeInBeds + time + timeRoom2Bed;
                            beds[i].usageTime+= patients[x].timeInBeds;
                            patients[x].bedsStTime = time + timeRoom2Bed;
                            patients[x].bedsFnTime = time + timeRoom2Bed + patients[x].timeInBeds;
                            patients[x].beds = i;
                            break;
                        }
                    }
                }
                for (; p < numPatients; ++p) {
                    for (int i = 0; i < numRoom; ++i) {
                        if (room[i].available == 0) {
                            room[i].available = 1;
                            room[i].patientId = p;
                            room[i].readyTime = patients[p].timeInRoom + time;
                            room[i].usageTime+= patients[p].timeInRoom;
                            patients[p].roomStTime = time;
                            patients[p].roomFnTime = time + patients[p].timeInRoom;
                            patients[p].room = i;
                            break;
                        }
                    }
                    if (patients[p].room == -1) {
                        break;
                    }
                }
            }
            out.format(" Patient          Operating Room          Recovery Room\n");
            out.format(" #  Name     Room#  Begin   End      Bed#  Begin    End\n");
            out.format(" ------------------------------------------------------\n");
            int begTime = 24 * 60;
            int endTime = 0;
            for (int i = 0; i < numPatients; ++i) {
                out.format("%2d  %-8s  %2d  %6s  %6s     %2d  %6s  %6s\n",
                    patients[i].indx + 1,
                    patients[i].name,
                    patients[i].room + 1,
                    format(patients[i].roomStTime),
                    format(patients[i].roomFnTime),
                    patients[i].beds + 1,
                    format(patients[i].bedsStTime),
                    format(patients[i].bedsFnTime));
                begTime = Math.min(begTime, patients[i].roomStTime);
                endTime = Math.max(endTime, patients[i].bedsFnTime);
            }
            out.format("\n");
            out.format("Facility Utilization\n");
            out.format("Type  # Minutes  %% Used\n");
            out.format("-------------------------\n");
            int workingTime = endTime - begTime;
            for (int i = 0; i < numRoom; ++i) {
                out.format("Room %2d %7d   %5s\n", i + 1, room[i].usageTime, format(room[i].usageTime, workingTime));
            }
            for (int i = 0; i < numBeds; ++i) {
                out.format("Bed  %2d %7d   %5s\n", i + 1, beds[i].usageTime, format(beds[i].usageTime, workingTime));
            }
            out.format("\n");
            return true;
        }

        private static String format(int minutes) {
            return String.format("%2d:%02d", minutes / 60, minutes % 60);
        }

        private static String format(int have, int total) {
            if (have == 0 || total == 0) {
                return "0.00";
            }
            return String.format("%.2f", 100.0 * have / total);
        }

        private static final class Patient {
            public final String name;
            public final int timeInRoom;
            public final int timeInBeds;
            public final int indx;
            public int roomStTime;
            public int roomFnTime;
            public int bedsStTime;
            public int bedsFnTime;
            public int room;
            public int beds;

            public Patient(int indx, String name, int timeInRoom, int timeInBeds) {
                this.indx = indx;
                this.name = name;
                this.timeInRoom = timeInRoom;
                this.timeInBeds = timeInBeds;
                this.room = -1;
                this.beds = -1;
            }
        }

        private static final class Facility {
            public int available;
            public int readyTime;
            public int usageTime;
            public int patientId;

            public static Facility[] create(int num) {
                Facility[] res = new Facility[num];
                for (int i = 0; i < num; ++i) {
                    res[i] = new Facility();
                }
                return res;
            }
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            boolean contd = true;
            for (int test = 1; in.hasNext(); ++test) {
                long beg = System.nanoTime();
                contd = new UseOfHospitalFacilities().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
            }
        }

        in.close();
        out.close();
    }

    private static final class InputReader {
        private BufferedReader reader;
        private StringTokenizer tokenizer;

        public InputReader(InputStream input) {
            reader = new BufferedReader(new InputStreamReader(input), 32768);
            tokenizer = null;
        }

        public boolean hasNext() {
            while (tokenizer == null || tokenizer.hasMoreTokens() == false) {
                try {
                    String nextLine = reader.readLine();
                    if (nextLine != null) {
                        tokenizer = new StringTokenizer(nextLine);
                    } else {
                        return false;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            return tokenizer.hasMoreTokens();
        }

        public String next() {
            if (hasNext()) {
                return tokenizer.nextToken();
            }
            return null;
        }

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public long nextLong() {
            return Long.parseLong(next());
        }

        public double nextDouble() {
            return Double.parseDouble(next());
        }

        public String nextLine() {
            try {
                return reader.readLine();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}
