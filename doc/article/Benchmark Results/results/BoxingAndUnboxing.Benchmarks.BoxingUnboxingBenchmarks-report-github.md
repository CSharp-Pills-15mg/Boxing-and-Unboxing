``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1288 (21H1/May2021Update)
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.402
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  Job-BDJHOG : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

Runtime=.NET 5.0  IterationCount=100  

```
|        Method |      Mean |     Error |    StdDev |    Median |
|-------------- |----------:|----------:|----------:|----------:|
| &#39;Simple Copy&#39; |  58.89 ms |  1.391 ms |  3.855 ms |  58.25 ms |
|        Boxing | 603.21 ms | 13.721 ms | 38.924 ms | 588.46 ms |
|      Unboxing |  73.67 ms |  0.990 ms |  2.695 ms |  73.34 ms |
