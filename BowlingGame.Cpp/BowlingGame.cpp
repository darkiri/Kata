#include "BowlingGame.h"
#include <algorithm>

namespace BowlingGame {

	CPPUNIT_TEST_SUITE_REGISTRATION (BowlingGameTests);

	void BowlingGameTests::RollMany(const int pins, const int times) {
		for (auto i = 0; i < times; i++) {
			_game.Roll(pins);
		}
	}

	void BowlingGameTests::GutterGameScoresToZero() {
		RollMany(0, 20);
		CPPUNIT_ASSERT_EQUAL(0, _game.Score());
	}

	void BowlingGameTests::SingleRollShouldScore() {
		_game.Roll(2);
		RollMany(0, 19);
		CPPUNIT_ASSERT_EQUAL(2, _game.Score());
	}

	void BowlingGameTests::EveryRollShouldScore() {
		RollMany(1, 20);
		CPPUNIT_ASSERT_EQUAL(20, _game.Score());
	}

	void BowlingGameTests::SingleSpareShouldScore() {
		_game.Roll(1);
		_game.Roll(9);
		_game.Roll(2);
		_game.Roll(3);
		RollMany(0, 16);
		CPPUNIT_ASSERT_EQUAL(17, _game.Score());
	}

	void BowlingGameTests::DoubleSpareShouldScore() {
		_game.Roll(1);
		_game.Roll(9);
		_game.Roll(1);
		_game.Roll(9);
		_game.Roll(2);
		_game.Roll(3);
		RollMany(0, 14);
		CPPUNIT_ASSERT_EQUAL(28, _game.Score());
	}

	void BowlingGameTests::SingleStrikeShouldScore() {
		_game.Roll(10);
		_game.Roll(2);
		_game.Roll(3);
		RollMany(0, 16);
		CPPUNIT_ASSERT_EQUAL(20, _game.Score());
	}

	void BowlingGameTests::DoubleStrikeShouldScore() {
		_game.Roll(10);
		_game.Roll(10);
		_game.Roll(2);
		_game.Roll(3);
		RollMany(0, 14);
		CPPUNIT_ASSERT_EQUAL(42, _game.Score());
	}

	void BowlingGameTests::PerfectGameShouldScoreTo300() {
		RollMany(10, 12);
		CPPUNIT_ASSERT_EQUAL(300, _game.Score());
	}

	void BowlingGame::Roll(const int pins) {
		_pins.push_back(pins); 

		if (pins == 10) {
			_pins.push_back(0);
		}
	}

	const int BowlingGame::Score() const {
		auto score = 0;
		for (auto frame = 0; frame < 10; ++frame){
			score += ScoreFrame(frame);
		}
		return score;
	}
	const int BowlingGame::ScoreFrame(const int num) const {
		if (IsStrike(num)) {
			return ScoreStrike(num);
		} else if (IsSpare(num)) {
			return ScoreSpare(num);
		} else {
			return ScoreNormal(num);
		}
	}

	const int BowlingGame::ScoreNormal(const int num) const { 
		auto frame = GetFrame(num);
		return get<0>(frame) + get<1>(frame);
	}

	const int BowlingGame::ScoreSpare(const int num) const { 
		auto nextFrame = GetFrame(num + 1);
		return 10 + get<0>(nextFrame);
	}

	const int BowlingGame::ScoreStrike(const int num) const { 
		auto strike_bonus = IsStrike(num+1) 
			? 10 + get<0>(GetFrame(num + 2))
			: ScoreNormal(num + 1);
		return 10 + strike_bonus;
	}

	const bool BowlingGame::IsSpare(const int num) const {
		return ScoreNormal(num) == 10;
	}

	const bool BowlingGame::IsStrike(const int num) const {
		return get<0>(GetFrame(num)) == 10;
	}

	const tuple<int, int> BowlingGame::GetFrame(int num) const {
		return make_tuple(_pins[num*2], _pins[num*2+1]);
	}
}
