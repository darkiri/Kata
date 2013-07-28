#include "BowlingGame.h"

namespace BowlingGame {

	void BowlingGame::roll(int p) {
		*cur_roll = p; 
		cur_roll++;

		auto cur_index = (cur_roll - &this->pins[0]);
		if (p == 10 && cur_index % 2 != 0) {
			roll(0);
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
		return frame.first + frame.second;
	}

	int BowlingGame::score_spare(int num) const { 
		auto nextFrame = get_frame(num + 1);
		return 10 + nextFrame.first;
	}

	int BowlingGame::score_strike(int num) const { 
		auto strike_bonus = is_strike(num+1) 
			? 10 + get_frame(num + 2).first
			: score_normal(num + 1);
		return 10 + strike_bonus;
	}

	bool BowlingGame::is_spare(int num) const {
		return score_normal(num) == 10;
	}

	bool BowlingGame::is_strike(int num) const {
		return get_frame(num).first == 10;
	}

	const pair<int, int>& BowlingGame::get_frame(int num) const {
		return make_pair(pins[num*2], pins[num*2+1]);
	}
}
