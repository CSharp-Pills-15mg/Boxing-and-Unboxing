# Boxing and Unboxing

## Problem Description

**Boxing**

- When an instance of a value type is cast to a reference type, a "coat" is created over the instance, in order to be perceived as a reference type. This is called boxing.

**Unboxing**

- Unboxing is the reversed process, of extracting the value from the "coat".

**Question**

Everybody knows that the boxing and unboxing are very time consuming. But, is it really true? This C# Pill is trying to answer this question:

- Are the boxing and unboxing processes really as time consuming as it is said?

## Setup

### Prerequisites

As a prerequisite:

- let's generate 10.000.000 integers and put them in a `List<int>`;

  ```csharp
  List<int> initialUnboxedNumbers = Enumerable.Range(0, count)
      .ToList();
  ```

- then generate another 10.000.000 integers and put them in a `List<object>`.

  ```csharp
  List<object> initialBoxedNumbers = Enumerable.Range(0, count)
      .Cast<object>()
      .ToList();
  ```

### Tests

For measuring the execution time we will use BenchmarkDotNet:

- https://github.com/dotnet/BenchmarkDotNet

We will create the `BoxingUnboxingBenchmarks` class:

```csharp
[SimpleJob(RuntimeMoniker.Net50, targetCount: 100)]
public class BoxingUnboxingBenchmarks
{
    ...
}
```

And three benchmark tests inside the benchmarks class:

- **Simple Copy (Control)** - A control test that will measure the time of copying the integers from `List<int>` into `List<int>`. No boxing.

  ```csharp
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
  ```

- **Boxing** - In this test we will measure the time of copying the integers from `List<int>` into `List<object>`.

  ```csharp
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
  ```

- **Unboxing** - In this test we will measure the time of copying the integers from `List<object>` into `List<int>`.

  ```csharp
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
  ```

## Running the Tests

Create a console application and run the benchmarks class:

```csharp
private static void Main(string[] args)
{
    BenchmarkRunner.Run<BoxingUnboxingBenchmarks>();
}
```

Run the console and enjoy the show for a few minutes...

## Results

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

## Conclusion

- Boxing is very time consuming. An order of magnitude slower than a simple copy.
- Unboxing, though, is much faster than boxing, just a little bit slower than a simple copy.
