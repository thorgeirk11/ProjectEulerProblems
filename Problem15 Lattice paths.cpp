#include <iostream>
#include <math.h> 
#include "BigIntegerLibrary.hh"

using namespace std;

void recCounter(int &counter, int sizeOfGrid, int right, int down)
{
	if (right < sizeOfGrid)
	{
		recCounter(counter, sizeOfGrid, right + 1, down);
	}
	if (down < sizeOfGrid)
	{
		recCounter(counter, sizeOfGrid, right, down + 1);
	}
	if (right == sizeOfGrid && down == sizeOfGrid)
	{
		counter++;
	}
}

int main()
{
	int counter = 0;
	int sizeOfGrid = 0;
	cin >> sizeOfGrid;
	recCounter(counter, sizeOfGrid, 0, 0);
	cout << counter;

	cin.get();
	cin.get();
	return 0;
}
