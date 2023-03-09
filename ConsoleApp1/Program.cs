using Otus.ArraySum;
using System.Diagnostics;

int arrayLength = 100000000;

var array = new int[arrayLength];

array.FillRandom(1, 50);

var stopwatch = Stopwatch.StartNew();

var result = array.CustomSum();

stopwatch.Stop();

Console.WriteLine(result);
Console.WriteLine(stopwatch.ElapsedMilliseconds);

stopwatch = Stopwatch.StartNew();
result = await array.ParallelSum(5);
stopwatch.Stop();
Console.WriteLine(result);
Console.WriteLine(stopwatch.ElapsedMilliseconds);

stopwatch = Stopwatch.StartNew();
result = array.ParallelSumWithLinq();

stopwatch.Stop();
Console.WriteLine(result);
Console.WriteLine(stopwatch.ElapsedMilliseconds);

Console.ReadLine();