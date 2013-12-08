CFLAGS = -std=c++0x -Wall 
CPATH = -I../packages/cppunit-1.12.1/include \
        -L../packages/cppunit-1.12.1/lib/linux 
LIBS = -lcppunit
CPP = main.cpp FindRuns.cpp
H = FindRuns.h

a.out: $(CPP)
	g++ -o $@ $(CFLAGS) $(CPATH) $(CPP) $(LIBS) 

$(CPP): $(H)
