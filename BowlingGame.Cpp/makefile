all: main.cpp BowlingGame.cpp BowlingGameTests.cpp
	g++ -std=c++0x -Wall -I../packages/cppunit-1.12.1/include -L../packages/cppunit-1.12.1/lib/linux main.cpp BowlingGame.cpp BowlingGameTests.cpp -lcppunit

BowlingGame.cpp: BowlingGame.h
BowlingGameTests.cpp: BowlingGameTests.h
BowlingGameTests.h : BowlingGame.h
clean:
