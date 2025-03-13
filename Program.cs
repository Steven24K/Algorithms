// See https://aka.ms/new-console-template for more information
var hashTable = new HashTable<int ,string>(10);

hashTable.Add(1, "Alice");
hashTable.Add(2, "Bob");
hashTable.Add(3, "Charles");
hashTable.Add(4, "Dirk");
hashTable.Add(5, "Eliot");

var value = hashTable.Find(4);
var not_found = hashTable.Find(777);

if (value == "Dirk") {
    Console.WriteLine($"You found the value = {value}");
}

if (not_found == null) {
    Console.WriteLine("Value not found");
}
 

Console.WriteLine("Hello, World!");