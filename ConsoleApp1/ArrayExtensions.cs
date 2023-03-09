using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.ArraySum
{
    public static class ArrayExtensions
    {
        public static void FillRandom(this int[] array, int min, int max)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(min, max);
            }
        }

        public static long CustomSum(this int[] array)
        {
            long result = 0;

            foreach (int item in array)
            {
                result += item;
            }

            return result;
        }

        public async static Task<long> ParallelSum(this int[] array, int threadCount)
        {
            var tasks = new List<Task<long>>();

            Func<int, int, long> sum = (int start, int end) =>
            {
                long result = 0;

                for (int i = start; i < Math.Min(array.Length, end); i++)
                {
                    result += array[i];
                }

                return result;
            };

            int batchSize = array.Length / threadCount;

            for (int i = 0; i < threadCount; i++)
            {
                int start = i * batchSize;
                int end = start + batchSize;
                tasks.Add(Task.Run<long>(() => sum(start, end)));
            }

            Task.WaitAll(tasks.ToArray());

            long result = 0;

            foreach (var item in tasks)
            {
                result += await item;
            }

            return result;
        }

        public static long ParallelSumWithLinq(this int[] array)
        {
            return array.AsParallel().Aggregate<int, long, long>(
            0,
            (acc, val) => acc + val,
            (acc1, acc2) => acc1 + acc2,
            acc => (long)acc
            );
        }
    }
}
