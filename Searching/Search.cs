public interface ISearch<T> where T : IComparable<T>
{
    static abstract int SequentialSearch(T[] data, T Item);
    static abstract int BinarySearch(T[] data, T Item);
    static abstract int BinarySearchRecursive(T[] data, T Item, int low, int high);
}

public class Search<T> : ISearch<T> where T : IComparable<T>
{
    public static int SequentialSearch(T[] data, T Item)
    {
        for (int i = 0; i < data.Length; i++)
            if (data[i].CompareTo(Item) == 0)
                return i;
        return -1;
    }

    public static int BinarySearch(T[] data, T Item)
    {
        int low = 0;
        int high = data.Length - 1;
        while (low <= high)
        {
            int middle = (low + high) / 2;
            if (Item.CompareTo(data[middle]) < 0)
                high = middle - 1;
            else if (Item.CompareTo(data[middle]) > 0)
                low = middle + 1;
            else
                return middle;
        }
        return -1;
    }

    public static int BinarySearchRecursive(T[] data, T Item, int low, int high)
    {
        if (low > high)
            return -1;
        int middle = (low + high) / 2;
        if (data[middle].CompareTo(Item) > 0)
            return BinarySearchRecursive(data, Item, low, middle - 1);
        else if (data[middle].CompareTo(Item) < 0)
            return BinarySearchRecursive(data, Item, middle + 1, high);
        else
            return middle;
    }
}
