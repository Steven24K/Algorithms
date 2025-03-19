public interface ISort<T> where T : IComparable<T>
{
    static abstract void InsertionSort(T[] data);
    static abstract void BubbleSort(T[] data);
    static abstract void MergeSort(T[] data, int low, int high);
    // MergeSort with Orderby 
}

public class Sort<T> : ISort<T> where T : IComparable<T>
{
    public static void BubbleSort(T[] data)
    {
        var somethingChanged = true;
        while (somethingChanged)
        {
            somethingChanged = false;
            for (int i = 0; i < data.Length - 1; i++)
            {
                if (data[i].CompareTo(data[i + 1]) > 0)
                {
                    var temp = data[i + 1];
                    data[i + 1] = data[i];
                    data[i] = temp;
                    somethingChanged = true;
                }
            }
        }
    }

    public static void InsertionSort(T[] data)
    {
        for (int j = 1; j < data.Length; j++)
        {
            var key = data[j];
            var i = j - 1;
            while (i >= 0 && data[i].CompareTo(key) > 0)
            {
                data[i + 1] = data[i];
                i = i - 1;
            }
            data[i + 1] = key;
        }
    }


    static public void MergeSort(T[] array, int low, int high)
    {
        if (low < high)
        {
            var middle = (low + high) / 2;
            MergeSort(array, low, middle);
            MergeSort(array, middle + 1, high);
            Merge(array, low, middle, high);
        }
    }

    static public void Merge(T[] array, int low, int middle, int high)
    {
        var lengthPart1 = middle - low + 1;
        var lengthPart2 = high - middle;
        T[] part1 = new T[lengthPart1];
        T[] part2 = new T[lengthPart2];
        for (int index = 0; index < lengthPart1; index++)
            part1[index] = array[low + index];
        for (int index = 0; index < lengthPart2; index++)
            part2[index] = array[middle + 1 + index];
        var i = 0;
        var j = 0;
        for (int k = low; k <= high; k++)
        {
            if (i < lengthPart1 && j < lengthPart2)
            {
                if (part1[i].CompareTo(part2[j]) <= 0)
                {
                    array[k] = part1[i];
                    i = i + 1;
                }
                else
                {
                    array[k] = part2[j];
                    j = j + 1;
                }
            }
            else if (i >= lengthPart1)
            {
                array[k] = part2[j];
                j = j + 1;
            }
            else
            {
                array[k] = part1[i];
                i = i + 1;
            }
        }
    }
}