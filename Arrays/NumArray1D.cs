using System.Numerics;

public interface INumArray1D<T> : IArray1D<T> where T : INumber<T>
{
    T? Aggregate(Func<T, T, T> fx);
    T? Sum();
    T? Min();
    T? Max();
    T? Prodcut(bool IgnoreZeros = true);

    //Map Reduce and Filter to come in future.
}
public class NumArray1D<T>:Array1D<T>, INumArray1D<T> where T: INumber<T>
{
  public NumArray1D(int size = 10):base(size) {  }
  public NumArray1D(T[] data):base(data) { }
    public T? Aggregate( Func<T, T, T> fx)
  {
    if(_data == null) return default(T);
    var acc = _data[0];
    for (int i = 1; i < Count; i++)
        acc = fx(acc, _data[i]);
    return acc;
  }

  
  public  T? Sum()=> Aggregate((_a,_b)=>_a+_b);
  public  T? Min()=> Aggregate((_a,_b)=>_a<= _b? _a:_b );
  public  T? Max()=> Aggregate((_a,_b)=>_a>= _b? _a:_b);
  
  public  T? Prodcut(bool IgnoreZeros=true){
    if(_data == null) return default(T);
    T? acc = default;
    T? zero =  default;
    for (int i = 0; i < Count; i++){
      if(IgnoreZeros && _data[i].Equals(zero)) continue;
      if(acc.Equals(zero)) 
        acc = _data[i];
      else
        acc = acc* _data[i];
    }
    return acc;
  }
}