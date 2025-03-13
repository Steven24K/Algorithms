using System.Collections.ObjectModel;

public interface IHashTable<K, V>{
    bool Add(K key, V value);
    V? Find(K key);
    bool Delete(K key);
}

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


public class Person
{
    public int Age { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }

    public Person(int age, string lastName, string firstName)
    {
        Age = age;
        LastName = lastName;
        FirstName = firstName;
    }

    public Person(Person p)
    {   
        if (!ReferenceEquals(p, null)) {
            Age = p.Age;
            LastName = p.LastName;
            FirstName = p.FirstName;
        }
    }

    public static bool operator==(Person p1, Person p2){
        if(ReferenceEquals(p1, null) && ReferenceEquals(p2, null) || ReferenceEquals(p1, p2))
            return true;
        if(!ReferenceEquals(p1, null))
            return p1.Equals(p2);
        return p2.Equals(p1);
    }

    public static bool operator!=(Person p1, Person p2) => !(p1 == p2);

    public override bool Equals(object? obj)
    {
        if(obj == null)
            return false;
        if(obj is Person p) 
            return ReferenceEquals(this, p) || p.Age == Age && p.LastName == LastName && p.FirstName == FirstName;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public class HashTable<K, V> : IHashTable<K, V>
{
    Entry<K, V>[]? buckets { get; set;}

    public ReadOnlyCollection<Entry<K, V>> data => buckets == null? null : buckets.AsReadOnly();

    public HashTable() { buckets = null; }

    public HashTable(Entry<K, V>[]? input) { importData(input);}

    public HashTable(int capacity)
    {
        buckets = new Entry<K, V>[capacity];
    }

    protected int getIndex(K key)
    {
        int hashCode = Math.Abs(key.GetHashCode());
        return hashCode % buckets.Length;
    }

    public bool Add(K key, V value)
    {
        if(buckets == null || buckets.Length == 0) return false;
        var idx = getIndex(key);

        if(Find(key) != null) return false;

        if(buckets[idx] == null){
          buckets[idx] = new Entry<K, V>(key, value);
          return true;
        }

        var potentialIdx = (idx + 1) % buckets.Length;

        while(potentialIdx != idx){
            if(buckets[potentialIdx] == null){
                buckets[potentialIdx] = new Entry<K, V>(key, value);
                return true;
            }
              
            potentialIdx = (potentialIdx + 1) % buckets.Length;
        }
        return false;
    }

    public V? Find(K key)
    {
        if(buckets == null || buckets.Length == 0) return default;
        var idx = getIndex(key);

        if(buckets[idx] != null && buckets[idx].Key.Equals(key))
           return buckets[idx].Value;
        
        var potentialIdx = (idx + 1) % buckets.Length;

        while(potentialIdx != idx){
            if(buckets[potentialIdx] != null && buckets[potentialIdx].Key.Equals(key))
              return buckets[potentialIdx].Value;
            potentialIdx = (potentialIdx + 1) % buckets.Length;
        }

        return default;
    }

    public Entry<K, V>? FindEntry(K key)
    {
        if(buckets == null || buckets.Length == 0) return default;
        var idx = getIndex(key);

        if(buckets[idx] != null && buckets[idx].Key.Equals(key))
           return buckets[idx];
        
        var potentialIdx = (idx + 1) % buckets.Length;

        while(potentialIdx != idx){
            if(buckets[potentialIdx] != null && buckets[potentialIdx].Key.Equals(key))
              return buckets[potentialIdx];
            potentialIdx = (potentialIdx + 1) % buckets.Length;
        }

        return default;
    }

    public bool Delete(K key)
    {
        if(buckets == null || buckets.Length == 0) return default;

        if(Find(key) == null) return false;
        
        var idx = getIndex(key);

        if(buckets[idx] != null && buckets[idx].Key.Equals(key)){
          buckets[idx] = default(Entry<K, V>);
          return true;
        }
        
        var potentialIdx = (idx + 1) % buckets.Length;

        while(potentialIdx != idx){
            if(buckets[potentialIdx] != null && buckets[potentialIdx].Key.Equals(key)){
                buckets[potentialIdx] = default(Entry<K, V>);
                return true;
            }
              
            potentialIdx = (potentialIdx + 1) % buckets.Length;
        }
        return false;
    }


    //DO NOT REMOVE the following method:
    private void importData(Entry<K, V>[]? inputData){
        if(inputData != null) {
            buckets = new Entry<K, V>[inputData.Length];
            for (int i = 0; i < inputData.Length; ++i) 
                buckets[i] = inputData[i];
        }
    }
}
