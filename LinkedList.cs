public interface ILinkedList<T>
{
    bool IsEmpty { get; }
    ILinkedList<T> AddToStart(T item);
    ILinkedList<T> Delete(T item);

    string PrintList();
}

public class ListNode<T> : ILinkedList<T>
{
    private T Value;
    public ILinkedList<T> Next;

    public ListNode(T _v, ILinkedList<T> _next)
    {
        Value = _v;
        Next = _next;
    }

    public bool IsEmpty => false;

    public ILinkedList<T> AddToStart(T item)
    {
        return new ListNode<T>(item, this);
    }

    public ILinkedList<T> Delete(T item)
    {
        return this;
    }

    public string PrintList()
    {
        return Value.ToString() + ", " + Next.PrintList();
    }
}

public class EmptyNode<T> : ILinkedList<T>
{
    public EmptyNode()
    {
    }

    public bool IsEmpty => true;

    public ILinkedList<T> AddToStart(T item)
    {
        return new ListNode<T>(item, this);
    }

    public ILinkedList<T> Delete(T item)
    {
        return this;
    }

    public string PrintList()
    {
        return "";
    }
}