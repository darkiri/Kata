#include "BowlingGame.h"
#include <algorithm>

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

	void BowlingGameTests::PerfectGameShouldScoreTo300() {
		RollMany(10, 12);
		CPPUNIT_ASSERT_EQUAL(300, _game.score());
	}

	void BowlingGame::roll(int pins) {
		*cur_roll = pins; 
		cur_roll++;

		if (pins == 10) {
			*cur_roll = 0; 
			cur_roll++;
		}
	}

	int BowlingGame::score() const {
		auto score = 0;
		for (auto frame = 0; frame < 10; ++frame){
			score += score_frame(frame);
		}
		return score;
	}
	int BowlingGame::score_frame(int num) const {
		if (is_strike(num)) {
			return score_strike(num);
		} else if (is_spare(num)) {
			return score_spare(num);
		} else {
			return score_normal(num);
		}
	}

	int BowlingGame::score_normal(int num) const { 
		auto frame = get_frame(num);
		return get<0>(frame) + get<1>(frame);
	}

	int BowlingGame::score_spare(int num) const { 
		auto nextFrame = get_frame(num + 1);
		return 10 + get<0>(nextFrame);
	}

	int BowlingGame::score_strike(int num) const { 
		auto strike_bonus = is_strike(num+1) 
			? 10 + get<0>(get_frame(num + 2))
			: score_normal(num + 1);
		return 10 + strike_bonus;
	}

	bool BowlingGame::is_spare(int num) const {
		return score_normal(num) == 10;
	}

	bool BowlingGame::is_strike(int num) const {
		return get<0>(get_frame(num)) == 10;
	}

	tuple<int, int> BowlingGame::get_frame(int num) const {
		return make_tuple(pins[num*2], pins[num*2+1]);
	}
}
