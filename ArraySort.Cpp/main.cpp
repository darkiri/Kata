#include <cppunit/BriefTestProgressListener.h>
#include <cppunit/CompilerOutputter.h>
#include <cppunit/extensions/TestFactoryRegistry.h>
#include <cppunit/TestResult.h>
#include <cppunit/TestResultCollector.h>
#include <cppunit/TestRunner.h>
#include <cppunit/TestCaller.h>
#include <cppunit/extensions/RepeatedTest.h>

#include "ArraySort.h"

using namespace CppUnit;
using namespace ArraySort;

int main( int argc, char* argv[] ) {
	TestResult controller;

	TestResultCollector result;
	controller.addListener( &result );        

	BriefTestProgressListener progress;
	controller.addListener( &progress );      

	auto arraySortCaller = new TestCaller<ArraySortTests>("ArraySort", 
			&ArraySortTests::sort_test);

	auto repeater = new RepeatedTest(arraySortCaller, 10);

	TestRunner runner;
	runner.addTest( repeater );
	runner.run( controller );

	CompilerOutputter outputter( &result, stdCOut() );
	outputter.write(); 

	return result.wasSuccessful() ? 0 : 1;
}
