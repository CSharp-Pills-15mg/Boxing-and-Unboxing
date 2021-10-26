using BenchmarkDotNet.Running;
using BoxingAndUnboxing.Benchmarks;

namespace BoxingAndUnboxing.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Assembly assembly = typeof(BoxingUnboxingBenchmarks).Assembly;
            //BenchmarkSwitcher.FromAssembly(assembly).Run(args);

            BenchmarkRunner.Run<BoxingUnboxingBenchmarks>();
        }
    }
}