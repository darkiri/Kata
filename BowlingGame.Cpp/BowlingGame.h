#ifndef BOWLING_GAME
#define BOWLING_GAME

#include <cppunit/extensions/HelperMacros.h>
#include <tuple>

using namespace std;

namespace BowlingGame {

	class BowlingGame {
		public:
			void Roll(const int);
			const int Score() const;
		private:
			vector<int> _pins;
			const tuple<int, int> GetFrame(const int num) const;
			const int ScoreFrame(const int frame) const;
			const bool IsSpare(const int frame) const;
			const bool IsStrike(const int frame) const;
			const int ScoreNormal(const int frame) const;
			const int ScoreSpare(const int frame) const;
			const int ScoreStrike(const int frame) const;
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
