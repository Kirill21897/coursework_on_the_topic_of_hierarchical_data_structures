using System;
using BinaryTreeExample;

class Program
{
    static void Main(string[] args)
    {
        var tree = new BinarySearchTree<int>();
        int[] values = { 5, 3, 7, 2, 4, 6, 8 };

        foreach (var value in values)
            tree.Insert(value);

        Console.WriteLine("In-order traversal:");
        tree.InOrderTraversal(Console.Write); // Вывод: 2 3 4 5 6 7 8
        Console.WriteLine();

        Console.WriteLine("\nPre-order traversal:");
        tree.PreOrderTraversal(Console.Write); // Вывод: 5 3 2 4 7 6 8
        Console.WriteLine();

        Console.WriteLine("\nPost-order traversal:");
        tree.PostOrderTraversal(Console.Write); // Вывод: 2 4 3 6 8 7 5
        Console.WriteLine();

        Console.WriteLine("\nContains 4: " + tree.Contains(4)); // True
        Console.WriteLine("Contains 9: " + tree.Contains(9)); // False

        tree.Delete(3);
        Console.WriteLine("\nAfter deleting 3 (In-order):");
        tree.InOrderTraversal(Console.Write); // Вывод: 2 4 5 6 7 8
        Console.WriteLine();
    }
}