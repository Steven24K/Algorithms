public static class Sorting
{
    public static void BubbleSort(int[] _arr)
    {
        int index = _arr.Length;
        bool sorted = false;
        while (index > 1 && !sorted)
        {
            sorted = true;
            for (int pointer = 1; pointer < index; pointer++)
            {
                if (_arr[pointer - 1] > _arr[pointer])
                {
                    int current = _arr[pointer - 1];
                    _arr[pointer - 1] = _arr[pointer];
                    _arr[pointer] = current;
                    sorted = false;
                }
            }
            index--;
        }
    }

    public static void InsertionSort(int[] _arr)
    {
        for (int i = 1; i < _arr.Length; i++)
        {
            int key = _arr[i];
            int j = i;
            while (j > 0 && _arr[j - 1] > key)
            {
                _arr[j] = _arr[j - 1];
                j--;
            }
            _arr[j] = key;
        }
    }

    static public void MergeSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            var middle = (low + high) / 2;
            MergeSort(array, low, middle);
            MergeSort(array, middle + 1, high);
            Merge(array, low, middle, high);
        }
    }

    static private void Merge(int[] array, int low, int middle, int high)
    {
        var lengthPart1 = middle - low + 1;
        var lengthPart2 = high - middle;
        int[] part1 = new int[lengthPart1];
        int[] part2 = new int[lengthPart2];
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