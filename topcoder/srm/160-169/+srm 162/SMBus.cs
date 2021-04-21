using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class SMBus {
        public int transmitTime(string[] messages, int[] times) {
            if (messages.Length > 0) {
                List<int> arbitrated = new List<int>();
                int result = 0;
                for (int bx = 0; true; ++bx) {
                    int time = transmit(messages, times, bx, arbitrated);
                    if (time > 0) {
                        result += time;
                    }
                    else break;
                }
                return result + transmitTime(filter(messages, arbitrated), filter(times, arbitrated));
            }
            return 0;
        }

        private int transmit(string[] messages, int[] times, int bx, List<int> arbitrated) {
            int result = 0, minbyte = 256;
            for (int i = 0; i < messages.Length; ++i) {
                if (bx < messages[i].Length) {
                    if (!arbitrated.Contains(i)) {
                        result = Math.Max(result, times[i]);
                        if (minbyte > Convert.ToInt32(messages[i][bx])) {
                            minbyte = Convert.ToInt32(messages[i][bx]);
                        }
                    }
                }
            }
            for (int i = 0; i < messages.Length; ++i) {
                if (bx < messages[i].Length) {
                    if (Convert.ToInt32(messages[i][bx]) > minbyte) {
                        if (!arbitrated.Contains(i)) {
                            arbitrated.Add(i);
                        }
                    }
                }
            }
            return result;
        }

        private T[] filter<T>(T[] obj, List<int> arbitrated) {
            List<T> result = new List<T>();
            foreach (int index in arbitrated) {
                result.Add(obj[index]);
            }
            return result.ToArray();
        }
    }
}