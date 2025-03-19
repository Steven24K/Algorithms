public class Entry<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }

    public Entry(K key, V value)
    {
        Key = key;
        Value = value;
    }
}

public class HashTable<K, V>
{
    public Entry<K, V>[]? buckets { get; set; }

    protected HashTable() { buckets = null; }

    public HashTable(int capacity)
    {
        buckets = new Entry<K, V>[capacity];
    }

    protected int getIndex(K key)
    {
        int hashCode = Math.Abs(key.GetHashCode());
        int index = hashCode % buckets.Length;
        return index;
    }

    public void Add(K key, V value)
    {
        int index = getIndex(key);
        if (buckets[index] == null) // the bucket is empty, we can insert
            buckets[index] = new Entry<K, V>(key, value);
        else // we have to do probing to find an empty bucket
        {
            var potentialIndex = (index + 1) % buckets.Length;
            while (buckets[potentialIndex] != null) // the bucket in position potentialIndex is not empty Potentially error
            {
                if (potentialIndex == index)
                    return;
                potentialIndex++;
                if (potentialIndex >= buckets.Length)
                    potentialIndex = 0;
            }
            buckets[potentialIndex] = new Entry<K, V>(key, value);
        }
    }

    public V Find(K key)
    {
        int index = getIndex(key);

        if (buckets[index] != null && buckets[index].Key.Equals(key)) // the hashed bucket contains the key we are looking for
        {
            return buckets[index].Value;
        }
        else // the key we are looking for could be in another position: use linear probing to find it
        {
            var potentialIndex = (index + 1) >= buckets.Length ? 0 : (index + 1);
            while (potentialIndex != index)
            {
                if (buckets[potentialIndex] != null && buckets[potentialIndex].Key.Equals(key))
                {
                    return buckets[potentialIndex].Value;
                }
                potentialIndex++;
                if (potentialIndex >= buckets.Length) //wraparound required
                    potentialIndex = 0;
            }
        }
        return default(V);
    }

    public void Delete(K key)
    {
        int index = getIndex(key);
        if (buckets[index] != null && buckets[index].Key.Equals(key)) //the hashed bucket is not empty and it contains the key that we want to delete
        {
            buckets[index] = null;
        }
        else //the key we want to delete could be in another position: use linear probing to find it.
        {
            var potentialIndex = (index + 1) >= buckets.Length ? 0 : (index + 1);
            while (potentialIndex != index)
            {
                if (buckets[potentialIndex] != null && buckets[potentialIndex].Key.Equals(key))
                {
                    buckets[potentialIndex] = null;
                    return;
                }

                potentialIndex++;
                if (potentialIndex >= buckets.Length)
                    potentialIndex = 0;
            }
        }
    }
}