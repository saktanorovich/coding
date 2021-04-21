using System;

namespace TopCoder.Algorithm {
    public class CrossWord {
        public int countWords(string[] board, int size) {
            var cumul = new int[100];
            foreach (var row in board) {
                foreach (var slot in row.Split(new[] { 'X' }, StringSplitOptions.RemoveEmptyEntries)) {
                    ++cumul[slot.Length];
                }
            }
            return cumul[size];
        }
    }
}