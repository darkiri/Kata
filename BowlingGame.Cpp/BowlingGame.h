#ifndef BOWLING_GAME
#define BOWLING_GAME

#include <cppunit/extensions/HelperMacros.h>
#include <tuple>

using namespace std;

namespace BowlingGame {
	class BowlingGame {
		public:
			void roll(const int);
			int score() const;

			BowlingGame() {
				cur_roll = &pins[0];
			}
		private:
			int pins[23];
			int* cur_roll;

			pair<int, int> get_frame(int num) const;
			int score_frame(int frame) const;
			bool is_spare(int frame) const;
			bool is_strike(int frame) const;
			int score_normal(int frame) const;
			int score_spare(int frame) const;
			int score_strike(int frame) const;
	};

}
#endif
