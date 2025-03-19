public interface IFibonacci
{
  static abstract int FibonacciRecursive(int n);
  static abstract int FibonacciIterative(int n);
  static abstract int FibonacciDynamic(int n, int[] storedResults);

}

public class Fibonacci : IFibonacci
{

    public static int FibonacciRecursive(int n)
    {
        if (n <= 0) return 0;
        if (n == 0 || n == 1)
            return 1;

        return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
    }

    public static int FibonacciIterative(int n)
    {
        if (n <= 0)
            return 0;
        else
        {
            var previousFib = 0;
            var currentFib = 1;
            for (int i = 0; i < n - 1; i++)
            {
                var newFib = previousFib + currentFib;
                previousFib = currentFib;
                currentFib = newFib;
            }
            return currentFib;
        }
    }
    public static int FibonacciDynamic(int n, int[] storedResults)
    {
        if (n < 0) return 0;
        if (storedResults[n] == 0)
        {
            var nMinOne = FibonacciDynamic(n - 1, storedResults);
            var nMinTwo = FibonacciDynamic(n - 2, storedResults);
            storedResults[n] = nMinOne + nMinTwo;
        }
        return storedResults[n];
    }



    public static void Debug()
    {
        int[] mapIntermediateResults = new int[15];
        // initialize the first two values in the array which we use as a map
        mapIntermediateResults[0] = 0;
        mapIntermediateResults[1] = 1;
        // the default value of int is 0, so at all other indices we will find a zero
        var result1 = FibonacciRecursive(6);
        var result2 = FibonacciIterative(7);
        var result3 = FibonacciDynamic(8, mapIntermediateResults);

        Console.WriteLine($"{result1},{result2},{result3}");
    }
}