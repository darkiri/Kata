#include "ArraySort.h"
#include <algorithm>

namespace ArraySort{
	void ArraySortTests::sort_test(){
		vector<int> to_sort[10] = {
			vector<int>(),
			vector<int>({ 1 }),
			vector<int>({ 1, 2 }),
			vector<int>({ 2, 1 }),
			vector<int>({ 1, 2, 3 }),
			vector<int>({ 2, 1, 3 }),
			vector<int>({ 1, 3, 2 }),
			vector<int>({ 2, 3, 1 }),
			vector<int>({ 3, 1, 2 }),
			vector<int>({ 3, 2, 1 }),
		};
		auto v = to_sort[case_num];
		my_sort(v.begin(), v.end());
		auto v1 = v;
		std::sort(v1.begin(), v1.end());

		CPPUNIT_ASSERT(equal(
				v.begin(), 
				v.end(), 
				v1.begin()));
	}

	template<class Iter>
	struct tr {
		typedef typename iterator_traits<Iter>::reference ref;
	};

	template<class Iter> 
	void my_sort(Iter first, Iter last) {
		while (first != last--) {
			auto cur = first;
			while (cur != last) {
				typename tr<Iter>::ref a = *cur++;
				typename tr<Iter>::ref b = *cur;
				if (a > b) {
					swap(a, b);
				}
			}
		}
	}
}
