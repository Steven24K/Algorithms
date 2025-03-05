public static class ArrayMethods
{
    public static int Find<T>(T[] _arr, T v)
    {
        for (int i = 0; i < _arr.Length; i++) {
            if (v != null && v.Equals(_arr[i])) return i;
        }
        return -1;
    }

    public static U Aggregate<T, U>(T[] _arr, Func<T, U, U> reducer) {
        U result = default!;
        for (int i = 0; i < _arr.Length; i++) {
            result = reducer(_arr[i], result);
        }
        return result;
    }
}