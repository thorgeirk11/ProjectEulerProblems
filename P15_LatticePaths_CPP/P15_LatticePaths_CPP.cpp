#include <map>
#include <iostream>
#include <tuple>  
#include <ctime>

using namespace std;

const int GridSize = 20;
map<tuple<int, int>, long long> CachedCells;

long long PathCount(tuple<int, int> cell)
{
	auto got = CachedCells.find(cell);
	if (got != CachedCells.end()) return got->second;
	int row = get<0>(cell), col = get<1>(cell);
	if (row == GridSize && col == GridSize) return 1;
	if (row > GridSize || col > GridSize) return 0;
	return CachedCells[cell] = PathCount(make_tuple(row, col + 1)) + PathCount(make_tuple(row + 1, col));
}

static double diffclock(clock_t clock1, clock_t clock2)
{
	double diffticks = clock1 - clock2;
	double diffms = (diffticks) / (CLOCKS_PER_SEC / 1000);
	return diffms;
}

int main()
{
	clock_t start = clock();
	cout << PathCount(make_tuple(0, 0)) << endl;
	printf("Time taken in millisecs: %f", diffclock(start, clock()));
	return 0;
}

