public class SingleNode<T> where T : IComparable<T>
{
    public T Value { get; private set; }
    public SingleNode<T>? Next { get; set; }

    public SingleNode(T value, SingleNode<T>? next = null)
    {
        Value = value;
        Next = next;
    }

}

public interface ILinkedList<T> : IEnumerable<T> where T : IComparable<T>
{
    void AddFirst(T value);
    void AddLast(T value);
    void AddSorted(T value); // New method for maintaining sorted order
    bool Remove(T value);
    SingleNode<T>? Search(T value);
    bool Contains(T value);
    void Clear();
    int Count { get; }
    new IEnumerator<T> GetEnumerator();
    // Other methods or properties as needed
}



public class SinglyLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    public SingleNode<T>? Head;
    private int count;

    public SinglyLinkedList()
    {
        Head = null;
        count = 0;
    }

    public int Count => count;

    public void AddFirst(T value)
    {
        Head = new SingleNode<T>(value, Head);
        count++;
    }

    public void AddLast(T value)
    {
        SingleNode<T> newNode = new SingleNode<T>(value);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            SingleNode<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    public bool Remove(T value)
    {
        if (Head == null)
            return false;

        if (Head.Value.Equals(value))
        {
            Head = Head.Next;
            count--;
            return true;
        }

        SingleNode<T> current = Head;
        while (current.Next != null)
        {
            if (current.Next.Value.Equals(value))
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public SingleNode<T>? Search(T value)
    {
        SingleNode<T>? current = Head;
        while (current != null)
        {
            if (current.Value.Equals(value))
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }
    public bool Contains(T value) => Search(value) != null;
    public void AddSorted(T value)
    {
        SingleNode<T> newNode = new SingleNode<T>(value);

        if (Head == null || Comparer<T>.Default.Compare(value, Head.Value) <= 0)
        {
            newNode.Next = Head;
            Head = newNode;
            count++;
            return;
        }

        SingleNode<T> current = Head;
        while (current.Next != null && Comparer<T>.Default.Compare(value, current.Next.Value) > 0)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
        count++;
    }

    public void Clear()
    {
        Head = null;
        count = 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        SingleNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
