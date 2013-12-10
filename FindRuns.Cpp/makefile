CFLAGS = -std=c++0x -Wall -MMD
CPATH = -I../packages/cppunit-1.12.1/include \
        -L../packages/cppunit-1.12.1/lib/linux
LIBS = -lcppunit
CPP = main.cpp FindRuns.cpp

a.out: $(CPP)
	g++ -o $@ $(CFLAGS) $(CPATH) $(CPP) $(LIBS)

include $(wildcard *.d)

clean:
	rm a.out
	rm *.d
