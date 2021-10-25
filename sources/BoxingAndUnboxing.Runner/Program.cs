using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BoxingAndUnboxing.Benchmarks;

namespace BoxingAndUnboxing.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Assembly assembly = Assembly.GetEntryAssembly();
            //BenchmarkSwitcher.FromAssembly(assembly).Run(args);

            Summary summary1 = BenchmarkRunner.Run<BoxingUnboxingBenchmarks>();
        }
    }
}