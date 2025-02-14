// See https://aka.ms/new-console-template for more information
using ArrayExtensions;

int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int indexFound = numbers._Find(50);


int[] arr1 = new int[] { 1, 2, 3 };
int[] arr2 = new int[] { 4, 5 };
var double_arr = arr1._Zip(arr2);

Console.WriteLine(double_arr._Print2DArray());

Console.WriteLine($"Found value at index: {indexFound}");

Console.WriteLine("Hello, World!");
