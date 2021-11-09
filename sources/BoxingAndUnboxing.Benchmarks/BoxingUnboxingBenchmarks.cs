// C# Pills 15mg
// Copyright (C) 2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DustInTheWind.BoxingAndUnboxing.Benchmarks
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