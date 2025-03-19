using System.Numerics;

public interface IMultiArray
{
  //2D Array
  //RowSum
  static abstract T[]? RowSum<T>(T[,] arr2D) where T : INumber<T>;
  //ColumSum
  static abstract T[]? ColSum<T>(T[,] arr2D) where T : INumber<T>;
  //Jagged Array
  //Row with Highest sum
  static abstract Tuple<int, T>? MaxRowIndexSum<T>(T[][] arrJagged) where T : INumber<T>;
  //Column with Highest sum 
  static abstract T?[] MaxCol<T>(T[][] arrJagged) where T : INumber<T>;

  //Split e.g. Tuple<int, int, int>[] RGB int [] R-value, [] G-values, [] B-values
  static abstract T[][]? Split<T>(Tuple<T, T, T>[] input);
  //Zip e.g  a = [1, 2, 3], b = [10, 12, 13] ==> [[1, 10], [2, 12], [3, 13]]
  static abstract T[,]? Zip<T>(T[]? a, T[]? b);
}

public class MultiArray : IMultiArray
{
    public static T[]? RowSum<T>(T[,] arr2D) where T : INumber<T>
    {
        if (arr2D == null || arr2D.Length == 0) return null;
        int rows = arr2D.GetLength(0);
        int cols = arr2D.GetLength(1);
        T[] output = new T[rows];
        for (int i = 0; i < rows; i++)
        {
            output[i] = arr2D[i, 0];
            for (int j = 1; j < cols; j++)
            {
                output[i] += arr2D[i, j];
            }
        }
        return output;
    }
    public static T[]? ColSum<T>(T[,] arr2D) where T : INumber<T>
    {
        if (arr2D == null || arr2D.Length == 0) return null;
        int rows = arr2D.GetLength(0);
        int cols = arr2D.GetLength(1);
        T[] output = new T[cols];
        for (int j = 0; j < cols; j++)
        {
            output[j] = arr2D[0, j];
            for (int i = 1; i < rows; i++)
            {
                output[j] += arr2D[i, j];
            }
        }
        return output;
    }

    public static Tuple<int, T>? MaxRowIndexSum<T>(T[][] arrJagged) where T : INumber<T>
    {
        if (arrJagged == null || arrJagged.Length == 0) return null;
        int MaxIndex = 0;
        T MaxSum = default!;
        T RowSum = default!;
        for (int i = 0; i < arrJagged.Length; i++)
        {
            RowSum = arrJagged[i][0];
            for (int j = 1; j < arrJagged[i].Length; j++)
                RowSum += arrJagged[i][j];
            if (i == 0) MaxSum = RowSum;
            if (RowSum.CompareTo(MaxSum) > 0)
            {
                MaxSum = RowSum;
                MaxIndex = i;
            }
        }
        return Tuple.Create(MaxIndex, MaxSum);
    }
    public static T?[] MaxCol<T>(T[][] arrJagged) where T : INumber<T>
    {
        if (arrJagged == null || arrJagged.Length == 0) return null!;
        int Rows = arrJagged.Length;
        int MaxLen = 0;
        for (int i = 0; i < Rows; i++)
        {
            if (arrJagged[i] != null)
                MaxLen = MaxLen > arrJagged[i].Length ? MaxLen : arrJagged[i].Length;
        }
        T?[] output = new T[Rows];
        int MaxIndex = 0;
        T MaxSum = default!;
        T ColSum = default!;
        for (int j = 0; j < MaxLen; j++)
        {
            ColSum = default!;
            for (int i = 1; i < Rows; i++)
            {
                if (arrJagged[i].Length > j)
                {
                    ColSum = ColSum != null ? ColSum + arrJagged[i][j] : arrJagged[i][j];
                }
            }
            if (ColSum > MaxSum)
            {
                MaxSum = ColSum;
                MaxIndex = j;
            }
        }
        //
        for (int k = 0; k < Rows; k++)
        {
            if (arrJagged[k].Length > MaxIndex)
                output[k] = arrJagged[k][MaxIndex];
        }
        return output;
    }



    public static T[][]? Split<T>(Tuple<T, T, T>[] input)
    {
        if (input == null || input.Length == 0) return null;
        var Len = input.Length;
        T[][] output = new T[3][];
        output[0] = new T[Len];
        output[1] = new T[Len];
        output[2] = new T[Len];

        for (int i = 0; i < Len; ++i)
        {
            output[0][i] = input[i].Item1;
            output[1][i] = input[i].Item2;
            output[2][i] = input[i].Item3;
        }
        return output;
    }

    public static T[,]? Zip<T>(T[]? a, T[]? b)
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
}