using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BoxingAndUnboxing.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net50, targetCount: 100)]
    public class BoxingUnboxingBenchmarks
    {
        private IReadOnlyList<int> initialUnboxedNumbers;
        private IReadOnlyList<object> initialBoxedNumbers;

        [GlobalSetup]
        public void GlobalSetup()
        {
            const int count = 10_000_000;

            initialUnboxedNumbers = Enumerable.Range(0, count)
                .ToList();

            initialBoxedNumbers = Enumerable.Range(0, count)
                .Cast<object>()
                .ToList();
        }

        [Benchmark(Description = "Simple Copy")]
        public List<int> Control()
        {
            List<int> copiedNumbers = new List<int>(initialUnboxedNumbers.Count);

            for (int i = 0; i < initialUnboxedNumbers.Count; i++)
            {
                int number = initialUnboxedNumbers[i];
                copiedNumbers.Add(number);
            }

            return copiedNumbers;
        }

        [Benchmark(Description = "Boxing")]
        public List<object> Boxing()
        {
            List<object> boxedNumbers = new List<object>(initialUnboxedNumbers.Count);

            for (int i = 0; i < initialUnboxedNumbers.Count; i++)
            {
                object number = initialUnboxedNumbers[i]; // boxing
                boxedNumbers.Add(number);
            }

            return boxedNumbers;
        }

        [Benchmark(Description = "Unboxing")]
        public List<int> Unboxing()
        {
            List<int> unboxedNumbers = new List<int>(initialBoxedNumbers.Count);

            for (int i = 0; i < initialBoxedNumbers.Count; i++)
            {
                int number = (int)initialBoxedNumbers[i]; // unboxing
                unboxedNumbers.Add(number);
            }

            return unboxedNumbers;
        }
    }
}