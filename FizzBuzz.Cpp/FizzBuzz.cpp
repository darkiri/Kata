#include "FizzBuzz.h"

namespace FizzBuzz {

	CPPUNIT_TEST_SUITE_REGISTRATION (FizzBuzzTests);

	string FizzBuzz(int num) {
		if (num % 15 == 0) {
			return "FizzBuzz";
		} if (num % 3 == 0) {
			return "Fizz";
		} else if (num % 5 == 0) {
			return "Buzz";
		} else {
			return to_string(num);
		}
	}

	void FizzBuzzTests::AssertFizzBuzz(const int num, const string expected) {
		auto out = FizzBuzz(num);
		CPPUNIT_ASSERT_EQUAL(expected, out);
	}
	void FizzBuzzTests::For_1_Print_1(){
		AssertFizzBuzz(1, "1");
	}
	void FizzBuzzTests::For_2_Print_2(){
		AssertFizzBuzz(2, "2");
	}
	void FizzBuzzTests::For_3_Print_Fizz(){
		AssertFizzBuzz(3, "Fizz");
	}
	void FizzBuzzTests::For_5_Print_Buzz() {
		AssertFizzBuzz(5, "Buzz");
	}
	void FizzBuzzTests::For_15_Print_FizzBuzz() {
		AssertFizzBuzz(15, "FizzBuzz");
	}
}
