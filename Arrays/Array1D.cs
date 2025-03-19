public interface IArray1D<T> where T : IEquatable<T>//IComparable<T>  //INumber //.net7
{
    int Length { get; }
    int Count { get; }  

    int Find(T Item, int startIndex=0);
}

public class Array1D<T> : IArray1D<T> where T:IEquatable<T> 
{
    protected T[] _data=null!;
    protected int _index;
   
    public Array1D(int size = 10) { 
        _data = new T[size];
        _index = 0;
    }

    public Array1D(T[] data) { 
        //Shallow or deep copy here
        _data = data; //Shallow copy
        _index = data.Length;
    }

    public int Length => _data.Length;
    public int Count => _index;

    public int Find(T Item, int startIndex = 0) {
        for(int i=startIndex; i<Count; i++)
            if (_data[i].Equals(Item)) return i;
        return -1;
    }

}