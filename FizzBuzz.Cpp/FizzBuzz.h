#ifndef FIZZ_BUZZ
#define FIZZ_BUZZ

#include <xstring>
#include <cppunit/extensions/HelperMacros.h>

using namespace std;

namespace FizzBuzz {

	class FizzBuzzTests : public CPPUNIT_NS::TestFixture {
	  CPPUNIT_TEST_SUITE( FizzBuzzTests );
	  CPPUNIT_TEST( For_1_Print_1 );
	  CPPUNIT_TEST( For_2_Print_2 );
	  CPPUNIT_TEST( For_3_Print_Fizz );
	  CPPUNIT_TEST( For_5_Print_Buzz );
	  CPPUNIT_TEST( For_15_Print_FizzBuzz );
	  CPPUNIT_TEST_SUITE_END();

	protected:
	  void For_1_Print_1();
	  void For_2_Print_2();
	  void For_3_Print_Fizz();
	  void For_5_Print_Buzz();
	  void For_15_Print_FizzBuzz();
	private:
	  void AssertFizzBuzz(const int num, const string expected);
	};

}
#endif
