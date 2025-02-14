namespace ArrayExtensions;

public static class ArrayExtensions
{
    public static int _Find<T>(this T[] array, T v)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(v)) return i;
        }
        return -1;
    }

    public static T[,]? _Zip<T>(this T[] a, T[] b)
    {
        if (a == null || b == null) return null;
        int sizeA = a.Length;
        int sizeB = b.Length;
        int size = sizeA > sizeB ? sizeA : sizeB;
        T[,] output = new T[size, 2];
        for (int i = 0; i < size; i++)
        {
            if (i < sizeA) output[i, 0] = a[i];
            if (i < sizeB) output[i, 1] = b[i];
        }
        return output;
    }

    public static string _Print2DArray<T>(this T[,] arr)
    {
        if (arr == null) return string.Empty;
        int rows = arr.GetLength(0);
        int cols = arr.GetLength(1);
        var result = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += arr[i, j];
                if (j < cols - 1) result += ", ";
            }
            result += "\n";
        }
        return result.ToString();
    }

    // It is called bubble sort because it simular to the process 
    // of different size bubble under water. Where biggest 'bubbles' (values)
    // come to the surface at first. 
    public static T[] BubbleSort<T>(this T[] array) 
    {
        // for every item in the array 
        //      check if current value is smaller than the previous. 
        //      If true, swap the values
        //      Else continue to check the next
        return array;
    }

    // This one is very simular to how most people sort a hand of playing cards for a poker game.
    public static T[] InsertionSort<T>(this T[] array)
    {
        // Start at the second element, so skip the first. 
        // For each element do the following. 
        // Check it against every item in the list. 
        // While the next is bigger than the current 
        // Insert it after the next item. 
        // Swap the 'next' with the current at the end.
        return array;
    }

    public static T[] _MergeSort<T>(this T[] array) where T : IComparable<T>
    {
        // If the array only has 1 item just return the single item

        // Find the middle, to split the array
        // Get the left part 
        // Get the right part 

        // Call Merge sort recursively, on the left and right part of the array. 
        // This process will continue until the array has a length of 1

        // Eventually sort the array with the Merge function, to bring the splitted array back together. 

        return array;
    }

    public static T[] _Merge<T>(this T[] array, T[] left, T[] right) where T : IComparable<T>
    {
        return array;
    }
}