public class UTHashTable
{ 

      [Fact]
      public void Test_Add()
      {
       
      }  
        
      [Fact]
      public void Test_AddCollisions()
      {
        
      }

      [Fact]
      public void Test_Find()
      {
          

    }

    [Fact]
    public void Test_Delete()
    {
          

    }

    string printHashTables(HashTable<string, Person> expectedHashTable, HashTable<string, Person> actualHashTable) {

        var res = $"\nExpected HashTable Keys:  (total keys present: {expectedHashTable.data.Count(_ => _ != null)})\n";

        if(expectedHashTable != null && expectedHashTable.data != null){
          for(int idx = 0; idx < expectedHashTable.data.Count; ++idx){
            var entry = expectedHashTable.data[idx];
            if(entry != null) {
              res += idx > 9 ? $"idx: {idx}   key: {entry.Key}\n" : $"idx: {idx}    key: {entry.Key}\n";
            }
            else {
              res += idx > 9 ? $"idx: {idx}     <NULL>\n" : $"idx: {idx}      <NULL>\n";
            }

          }
        }

        if( actualHashTable != null && actualHashTable.data != null && actualHashTable.data.Count(_ => _ != null) != 0){
          res += $"\nActual HashTable Keys:    (total keys present: {actualHashTable.data.Count(_ => _ != null)})\n";
          for(int idx = 0; idx < actualHashTable.data.Count; ++idx){
            var entry = actualHashTable.data[idx];
            if(entry != null) {
              res += idx > 9 ? $"idx: {idx}   key: {entry.Key} value: {(entry.Key == expectedHashTable.data[idx]?.Key && entry.Value == expectedHashTable.data[idx]?.Value? "OK" : "-MISMATCH-")}\n" : 
                               $"idx: {idx}    key: {entry.Key} value: {(entry.Key == expectedHashTable.data[idx]?.Key && entry.Value == expectedHashTable.data[idx]?.Value? "OK" : "-MISMATCH-")}\n"; 
            }
            else {
              res += idx > 9 ? $"idx: {idx}     <NULL>\n" : $"idx: {idx}      <NULL>\n";
            }

          }
        }
        else
           res += "\n---------No data added into the hashtable---------"; 

        return res;
    }

}