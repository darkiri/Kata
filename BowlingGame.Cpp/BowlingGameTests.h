#ifndef BOWLING_GAME_TESTS
#define BOWLING_GAME_TESTS

#include "BowlingGame.h"

namespace BowlingGame {
	class BowlingGameTests : public CPPUNIT_NS::TestFixture {
		CPPUNIT_TEST_SUITE( BowlingGameTests );
		CPPUNIT_TEST( GutterGameScoresToZero );
		CPPUNIT_TEST( SingleRollShouldScore );
		CPPUNIT_TEST( EveryRollShouldScore );
		CPPUNIT_TEST( SingleSpareShouldScore );
		CPPUNIT_TEST( DoubleSpareShouldScore );
		CPPUNIT_TEST( SingleStrikeShouldScore );
		CPPUNIT_TEST( DoubleStrikeShouldScore );
		CPPUNIT_TEST( Zero_TenIsNotAStrike );
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
		void Zero_TenIsNotAStrike();
		void PerfectGameShouldScoreTo300();
		private:
		BowlingGame _game;

		void RollMany(const int, const int);
	};
}
#endif
