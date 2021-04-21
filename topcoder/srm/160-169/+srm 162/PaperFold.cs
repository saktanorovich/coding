using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class PaperFold {
        public int numFolds(int[] paper, int[] box) {
            return numFolds(new Rectangle(paper), new Rectangle(box));
        }

        private int numFolds(Rectangle paper, Rectangle obox) {
            int result = int.MaxValue; ;
            foreach (Rectangle box in obox.orientations()) {
                result = Math.Min(result, box.numFolds(paper));
            }
            if (result > 8) {
                return -1;
            }
            return result;
        }

        private class Rectangle {
            public readonly int[] dim;

            public Rectangle(int[] dim) {
                this.dim = dim;
            }

            public Rectangle[] orientations() {
                return new Rectangle[] {
                    new Rectangle(new int[] { dim[0], dim[1] }),
                    new Rectangle(new int[] { dim[1], dim[0] }),
                };
            }

            public int numFolds(Rectangle other) {
                int result = 0;
                for (int k = 0; k < 2; ++k) {
                    while (dim[k] < other.dim[k]) {
                        dim[k] *= 2;
                        ++result;
                    }
                }
                return result;
            }
        }
    }
}