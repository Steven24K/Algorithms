using System.Collections;

public class DoubleNode<T> where T : IComparable<T>
{
    public T Value { get; private set; }
    public DoubleNode<T>? Next { get; set; }
    public DoubleNode<T>? Previous { get; set; }

    public DoubleNode(T value, DoubleNode<T>? next = null, DoubleNode<T>? previous = null)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }
}

public interface IDoublyLinkedList<T> : IEnumerable<T> where T : IComparable<T>
{
    DoubleNode<T>? Search(T value);
    void AddFirst(T value); //good
    void AddLast(T value);  // good 
    void AddSorted(T value); // New method for maintaining sorted order
    bool Remove(T value);
    void Clear();
    new IEnumerator<T> GetEnumerator();
    // Other methods or properties as needed
}

public class DoublyLinkedList<T> : IDoublyLinkedList<T> where T : IComparable<T>
{
    public DoubleNode<T>? First, Last;
    public DoublyLinkedList() => First = Last = null;
    public void Clear() => First = null;

    //Search
    public DoubleNode<T>? Search(T value)
    {
        DoubleNode<T>? curr = First;
        while (curr != null && !curr.Value.Equals(value))
        {
            curr = curr.Next;
        }
        return curr;
    }



    #region "addNode=> first, last, sorted" 
    public void AddFirst(T value)
    {
        DoubleNode<T> newNode = new DoubleNode<T>(value, First, null);
        if (First != null) // check First
            First.Previous = newNode;
        First = newNode;
        if (Last == null) // check Last
            Last = newNode;
    }

    public void AddLast(T value)
    {
        DoubleNode<T> newNode = new DoubleNode<T>(value, null, Last);
        if (Last != null) // last node is not null
            Last.Next = newNode;
        Last = newNode;
        if (First == null) // first node is null
            First = newNode;
    }

    public void AddSorted(T value) // Not fixed yet
    {
        DoubleNode<T> newNode = new DoubleNode<T>(value);

        if (First == null || value.CompareTo(First.Value) < 0)

        {
            newNode.Next = First;
            if (First != null)
            {// check First
                First.Previous = newNode;
            }
            if (Last == null) // check Last
                Last = newNode;

            First = newNode;
            return;
        }

        DoubleNode<T>? current = First;
        while (current.Next != null && value.CompareTo(current.Next.Value) >= 0)
        {
            current = current.Next;
        }

        // place the node at the appropriate location
        newNode.Next = current.Next;
        current.Next = newNode;
        newNode.Previous = current;

        if (newNode.Next != null)
            newNode.Next.Previous = newNode;

        if (Last == null || Last == current) // check if node was the last one
            Last = newNode;


    }
    #endregion

    public bool Remove(T value)
    {
        var x = Search(value);
        if (x == null) return false;
        Delete(x);
        return true;
    }

    public void Delete(DoubleNode<T> node)
    {
        if (node.Previous != null) // check Prev
            node.Previous.Next = node.Next;
        if (node.Next != null) // check Next
            node.Next.Previous = node.Previous;
        if (First == node) // check First
            First = node.Next;
        if (Last == node) // check Last
            Last = node.Previous;
    }

    public IEnumerator<T> GetEnumerator()
    {
        DoubleNode<T>? current = First;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
