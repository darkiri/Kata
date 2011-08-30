using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BowlingGame {
    [TestFixture]
    public class BowlingGameTests {
        private Game _game;

        [SetUp]
        public void SetUp() {
            _game = new Game();
        }

        [Test]
        public void AllZeroesGameScoresToZero() {
            RollMany(0, 20);
            Assert.That(_game.Score(), Is.EqualTo(0));
        }

        private void RollMany(int pins, int times) {
            for (var i = 0; i < times; i++) {
                _game.Roll(pins);
            }
        }

        [Test]
        public void SingleRollShouldScore() {
            Roll(1);
            RollMany(0, 19);
            Assert.That(_game.Score(), Is.EqualTo(1));
        }

        private void Roll(params int[] pins) {
            foreach (var pin in pins) {
                _game.Roll(pin);
            }
        }

        [Test]
        public void EveryRollShouldScore() {
            RollMany(1, 20);
            Assert.That(_game.Score(), Is.EqualTo(20));
        }

        [Test]
        public void SingleSpareShouldScore() {
            Roll(1, 9, 2, 3);
            RollAllZeroes(16);
            Assert.That(_game.Score(), Is.EqualTo(12 + 5));
        }

        private void RollAllZeroes(int times) {
            RollMany(0, times);
        }

        [Test]
        public void DoubleSpareShouldScore() {
            Roll(1, 9, 2, 8, 3, 2);
            RollAllZeroes(14);
            Assert.That(_game.Score(), Is.EqualTo(12 + 13 + 5));
        }

        [Test]
        public void SingleStrikeShouldScore() {
            Roll(10, 1, 2);
            RollAllZeroes(16);
            Assert.That(_game.Score(), Is.EqualTo(13 + 3));
        }

        [Test]
        public void ZeroTenShouldBeScoredAsSpare() {
            Roll(0, 10, 10, 2, 1);
            RollAllZeroes(14);
            Assert.That(_game.Score(), Is.EqualTo(20 + 13 + 3));
        }

        [Test]
        public void DoubleStrikeShouldScore() {
            Roll(10, 10, 2, 1);
            RollAllZeroes(14);
            Assert.That(_game.Score(), Is.EqualTo(22 + 13 + 3));
        }

        [Test]
        public void PerfectGameShouldScoreTo300() {
            RollMany(10, 12);
            Assert.That(_game.Score(), Is.EqualTo(300));
        }
    }


    public class Game {
        private readonly List<int> _pins = new List<int>();

        public void Roll(int pins) {
            _pins.Add(pins);
            if (pins == 10 && _pins.Count % 2 == 1) {
                _pins.Add(0);
            }
        }

        public int Score() {
            return Enumerable
                .Range(0, 10)
                .Select(ScoreFrame)
                .Sum();
        }

        private int ScoreFrame(int frameNum) {
            return IsSpare(frameNum)
                       ? ScoreSpare(frameNum)
                       : IsStrike(frameNum)
                             ? ScoreStrike(frameNum)
                             : ScoreNormal(frameNum);
        }

        private bool IsSpare(int frameNum) {
            return !IsStrike(frameNum) && ScoreNormal(frameNum) == 10;
        }

        private bool IsStrike(int frameNum) {
            return GetFrame(frameNum).Item1 == 10;
        }

        private int ScoreSpare(int frameNum) {
            return ScoreNormal(frameNum) + GetFrame(frameNum + 1).Item1;
        }

        private int ScoreNormal(int frameNum) {
            return GetFrame(frameNum).Item1 + GetFrame(frameNum).Item2;
        }

        private int ScoreStrike(int frameNum) {
            return 10 + ScoreStrikeBonus(frameNum + 1);
        }

        private int ScoreStrikeBonus(int nextFrame) {
            return IsStrike(nextFrame)
                       ? 10 + GetFrame(nextFrame + 1).Item1
                       : ScoreNormal(nextFrame);
        }

        private Tuple<int, int> GetFrame(int frameNum) {
            return new Tuple<int, int>(_pins[2 * frameNum], _pins[2 * frameNum + 1]);
        }
    }
}