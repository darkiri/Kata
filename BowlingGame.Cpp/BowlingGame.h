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

			tuple<int, int> get_frame(int num) const;
			int score_frame(int frame) const;
			bool is_spare(int frame) const;
			bool is_strike(int frame) const;
			int score_normal(int frame) const;
			int score_spare(int frame) const;
			int score_strike(int frame) const;
	};

	class BowlingGameTests : public CPPUNIT_NS::TestFixture {
		CPPUNIT_TEST_SUITE( BowlingGameTests );
		CPPUNIT_TEST( GutterGameScoresToZero );
		CPPUNIT_TEST( SingleRollShouldScore );
		CPPUNIT_TEST( EveryRollShouldScore );
		CPPUNIT_TEST( SingleSpareShouldScore );
		CPPUNIT_TEST( DoubleSpareShouldScore );
		CPPUNIT_TEST( SingleStrikeShouldScore );
		CPPUNIT_TEST( DoubleStrikeShouldScore );
		CPPUNIT_TEST( PerfectGameShouldScoreTo300 );
		CPPUNIT_TEST_SUITE_END();

		protected:
		void GutterGameScoresToZero();
		void SingleRollShouldScore();
		void EveryRollShouldScore();
		void SingleSpareShouldScore();
		void DoubleSpareShouldScore();
		void SingleStrikeShouldScore();
		void DoubleStrikeShouldScore();
		void PerfectGameShouldScoreTo300();
		private:
		BowlingGame _game;

		void RollMany(const int, const int);
	};
}
#endif
