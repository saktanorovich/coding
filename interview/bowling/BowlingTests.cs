using System;
using NUnit.Framework;

namespace P.Bowling.Core.Tests {
    [TestFixture]
    public class BowlingTests {
        public Bowling Game { get; private set; }

        [SetUp]
        public void Setup() {
            Game = new Bowling();
        }

        [Test]
        public void Test01() {
            for (var i = 1; i <= 12; ++i) {
                Game.Roll(10);
            }
            Assert.AreEqual(300, Game.Score());
        }

        [Test]
        public void Test02() {
            for (var i = 1; i <= 20; ++i) {
                Game.Roll(0);
            }
            Assert.AreEqual(0, Game.Score());
        }

        [Test]
        public void Test03() {
            for (var i = 1; i <= 10; ++i) {
                Game.Roll(9);
                Game.Roll(1);
            }
            Game.Roll(10);
            Assert.AreEqual(191, Game.Score());
        }

        [Test]
        public void Test04() {
            for (var i = 1; i <= 9; ++i) {
                Game.Roll(0);
                Game.Roll(0);
            }
            Game.Roll(2);
            Game.Roll(9);
            Assert.Throws(typeof(Exception), () => Game.Score());
        }

        [Test]
        public void Test05() {
            for (var i = 1; i <= 9; ++i) {
                Game.Roll(0);
                Game.Roll(0);
            }
            Game.Roll(9);
            Game.Roll(2);
            Assert.Throws(typeof(Exception), () => Game.Score());
        }

        [Test]
        public void Test06() {
            Game.Roll(9);
            Game.Roll(9);
            Assert.Throws(typeof(Exception), () => Game.Score());
        }

        [Test]
        public void Test07() {
            Assert.AreEqual(0, Game.Score());
        }

        [Test]
        public void Test08() {
            Test(new[] {
                new[] {1, 4},
                new[] {4, 5},
                new[] {6, 4},
                new[] {5, 5},
                new[] {10},
                new[] {0, 1},
                new[] {7, 3},
                new[] {6, 4},
                new[] {10},
                new[] {2, 8, 6}
            }, 133);
        }

        [Test]
        public void Test09() {
            Test(new[] {
                new[] {2, 8},
                new[] {6, 4},
                new[] {10},
                new[] {10},
                new[] {9, 1},
                new[] {0, 1},
                new[] {7, 3},
                new[] {6, 4},
                new[] {10},
                new[] {10, 9, 1}
            }, 181);
        }

        [Test]
        public void Test10() {
            Test(new[] {
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5}
            }, 145);
        }

        [Test]
        public void Test11() {
            Test(new[] {
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5},
                new[] {5, 5, 5}
            }, 150);
        }

        [Test]
        public void Test12() {
            for (var i = 1; i <= 9; ++i) {
                Game.Roll(0);
                Game.Roll(0);
            }
            Game.Roll(10);
            Game.Roll(10);
            Game.Roll(10);
            Assert.AreEqual(30, Game.Score());
        }

        [Test]
        public void Test13() {
            for (var i = 1; i <= 9; ++i) {
                Game.Roll(9);
                Game.Roll(1);
            }
            Game.Roll(10);
            Game.Roll(10);
            Game.Roll(10);
            Assert.AreEqual(202, Game.Score());
        }

        [Test]
        public void Test14() {
            for (var i = 1; i <= 10; ++i) {
                Game.Roll(0);
                Game.Roll(1);
            }
            Assert.AreEqual(10, Game.Score());
        }

        [Test]
        public void Test15() {
            Test(new[] { 9, 1, 0, 10, 10, 10, 6, 2, 7, 3, 8, 2, 10, 9, 0, 10, 10, 8 }, 176);
        }

        [Test]
        public void Test16() {
            Test(new[] { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 }, 68);
        }

        [Test]
        public void Test17() {
            Assert.Throws(typeof(Exception), () => {
                Game.Roll(+1);
                Game.Roll(-1);
            });
        }

        [Test]
        public void Test18() {
            for (var i = 0; i < 10; ++i) {
                Game.Roll(6);
                Game.Roll(4);
            }
            Game.Roll(6);
            Assert.AreEqual(160, Game.Score());
        }

        private void Test(int[] history, int expected) {
            foreach (var pins in history) {
                Game.Roll(pins);
            }
            Assert.AreEqual(expected, Game.Score());
        }

        private void Test(int[][] history, int expected) {
            foreach (var frame in history) {
                foreach (var pins in frame) {
                    Game.Roll(pins);
                }
            }
            Assert.AreEqual(expected, Game.Score());
        }
    }
}
