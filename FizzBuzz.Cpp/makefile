all: main.cpp FizzBuzz.cpp
	g++ -std=c++0x -o FizzBuzz -Wall -I../packages/cppunit-1.12.1/include -L../packages/cppunit-1.12.1/lib/linux main.cpp FizzBuzz.cpp -lcppunit

FizzBuzz.cpp: FizzBuzz.h
clean:
	rm FizzBuzz *.o
