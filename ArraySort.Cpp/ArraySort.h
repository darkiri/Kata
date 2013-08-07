#ifndef ARRAY_SORT
#define ARRAY_SORT

#include <cppunit/extensions/HelperMacros.h>

using namespace std;

namespace ArraySort{

	template <class Iter>
	void my_sort(Iter first, Iter last);

	class ArraySortTests : public CPPUNIT_NS::TestFixture {
		public:
		void sort_test();

		void tearDown() {
			case_num++;
		}
		private:
		int case_num;
	};
}
#endif
