# Boxing and Unboxing

## Preparation Recipe

- Create a list of 10.000.000 integers (`List<int>`).
- a) Copy all numbers into a second list of integers. This will be our control measurement.
  - `List<int>` --> `List<int>`
- b) Copy the same numbers into a list of objects.
  - `List<int>` --> `List<object>`
- c) Copy the numbers from the list of objects into another list of integers.
  - `List<object>` --> `List<int>`
- Use a `Stopwatch` to measure the elapsed time for each of the list copy action.
  - Display the elapsed times.