namespace Test;

public class TestArrayMethods
{
    [Fact]
    public void TestFind()
    {
        int[] numbers = {1, 2, 3, 4, 5};
        int result1 = ArrayMethods.Find(numbers, 4);
        int result2 = ArrayMethods.Find(numbers, 42);

        Assert.Equal(3, result1);
        Assert.Equal(-1, result2);
    }
}