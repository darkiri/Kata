#ifndef FIND_RUNS
#define FIND_RUNS

#include <cppunit/extensions/HelperMacros.h>

using namespace std;

namespace Kata {
    class FindRunsTests : public CPPUNIT_NS::TestFixture {
        CPPUNIT_TEST_SUITE( FindRunsTests );
        CPPUNIT_TEST( test1 );
        CPPUNIT_TEST_SUITE_END();
        protected:
        void test1();
    };
}
#endif
