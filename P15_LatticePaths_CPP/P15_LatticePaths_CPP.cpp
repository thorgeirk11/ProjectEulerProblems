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

int main()
{
	clock_t start = clock();
	for (size_t i = 0; i < 10000; i++)
	{
		CachedCells.clear();
		PathCount(make_tuple(0, 0));
	}
	clock_t end = clock();
	cout << "Time taken in millisecs: " << end - start;
	cin.get();
	cin.get();
	return 0;
}

