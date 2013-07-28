#include "BowlingGameTests.h"

namespace BowlingGame {
	CPPUNIT_TEST_SUITE_REGISTRATION (BowlingGameTests);

	void BowlingGameTests::RollMany(const int pins, const int times) {
		for (auto i = 0; i < times; i++) {
			_game.roll(pins);
		}
	}

	void BowlingGameTests::GutterGameScoresToZero() {
		RollMany(0, 20);
		CPPUNIT_ASSERT_EQUAL(0, _game.score());
	}

	void BowlingGameTests::SingleRollShouldScore() {
		_game.roll(2);
		RollMany(0, 19);
		CPPUNIT_ASSERT_EQUAL(2, _game.score());
	}

	void BowlingGameTests::EveryRollShouldScore() {
		RollMany(1, 20);
		CPPUNIT_ASSERT_EQUAL(20, _game.score());
	}

	void BowlingGameTests::SingleSpareShouldScore() {
		_game.roll(1);
		_game.roll(9);
		_game.roll(2);
		_game.roll(3);
		RollMany(0, 16);
		CPPUNIT_ASSERT_EQUAL(17, _game.score());
	}

	void BowlingGameTests::DoubleSpareShouldScore() {
		_game.roll(1);
		_game.roll(9);
		_game.roll(1);
		_game.roll(9);
		_game.roll(2);
		_game.roll(3);
		RollMany(0, 14);
		CPPUNIT_ASSERT_EQUAL(28, _game.score());
	}

	void BowlingGameTests::SingleStrikeShouldScore() {
		_game.roll(10);
		_game.roll(2);
		_game.roll(3);
		RollMany(0, 16);
		CPPUNIT_ASSERT_EQUAL(20, _game.score());
	}

	void BowlingGameTests::DoubleStrikeShouldScore() {
		_game.roll(10);
		_game.roll(10);
		_game.roll(2);
		_game.roll(3);
		RollMany(0, 14);
		CPPUNIT_ASSERT_EQUAL(42, _game.score());
	}

	void BowlingGameTests::Zero_TenIsNotAStrike() {
		_game.roll(0);
		_game.roll(10);
		_game.roll(2);
		_game.roll(3);
		RollMany(0, 16);
		CPPUNIT_ASSERT_EQUAL(17, _game.score());
	}

	void BowlingGameTests::PerfectGameShouldScoreTo300() {
		RollMany(10, 12);
		CPPUNIT_ASSERT_EQUAL(300, _game.score());
	}
}
