# Boxing and Unboxing

## Preparation Recipe

- Create a class library.
- Include `BenchmarkDotNet` nuget package.

- Create the `BoxingUnboxingBenchmark` class with `targetCount` of 10:

  ```csharp
  [SimpleJob(RuntimeMoniker.Net50, targetCount: 10)]
  public class BoxingUnboxingBenchmarks
  {
      ...
  }
  ```

- **Benchmark Initialization** - Add a `GlobalSetup` method to create two lists; one of integers, one of objects and store the same 10.000.000 numbers in both of them.

  ```csharp
  private List<int> unboxedNumbers = Enumerable.Range(0, 10_000_000).ToList();
  private List<object> boxedNumbers = Enumerable.Range(0, 10_000_000).Cast<object>().ToList();
  ```

- **Control benchmark** - Create a control benchmark that copies the numbers without boxing or unboxing

  - `List<int>` --> `List<int>`

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

- **Boxing benchmark** - Create a benchmark that boxes the numbers when copies them.

  - `List<int>` --> `List<object>`

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

- **Unboxing benchmark** - Create a benchmark that unboxes the numbers when copies them.

  - `List<object>` --> `List<int>`

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

- **Run Tests** - Create a Console Application to run the benchmarks.

  - Add `BenchmarkDotNet` nuget package.

  - Add reference to the class library created previously

  - Call the benchmark class:

    ```csharp
    private static void Main(string[] args)
    {
        BenchmarkRunner.Run<BoxingUnboxingBenchmarks>();
    }
    ```

  - Run the application.