using System;

namespace BinaryTreeExample
{
    // Узел бинарного дерева
    public class BinaryTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T value)
        {
            Value = value;
        }
    }

    // Бинарное дерево поиска
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T> Root { get; private set; }

        // Добавление элемента
        public void Insert(T value)
        {
            Root = InsertRecursive(Root, value);
        }

        private BinaryTreeNode<T> InsertRecursive(BinaryTreeNode<T> node, T value)
        {
            if (node == null) return new BinaryTreeNode<T>(value);

            if (value.CompareTo(node.Value) < 0)
                node.Left = InsertRecursive(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = InsertRecursive(node.Right, value);

            return node;
        }

        // Поиск элемента
        public bool Contains(T value)
        {
            return ContainsRecursive(Root, value);
        }

        private bool ContainsRecursive(BinaryTreeNode<T> node, T value)
        {
            if (node == null) return false;
            if (value.CompareTo(node.Value) == 0) return true;

            return value.CompareTo(node.Value) < 0 
                ? ContainsRecursive(node.Left, value) 
                : ContainsRecursive(node.Right, value);
        }

        // Удаление элемента
        public void Delete(T value)
        {
            Root = DeleteRecursive(Root, value);
        }

        private BinaryTreeNode<T> DeleteRecursive(BinaryTreeNode<T> node, T value)
        {
            if (node == null) return null!;

            if (value.CompareTo(node.Value) < 0)
                node.Left = DeleteRecursive(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = DeleteRecursive(node.Right, value);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                node.Value = MinValue(node.Right);
                node.Right = DeleteRecursive(node.Right, node.Value);
            }

            return node;
        }

        private T MinValue(BinaryTreeNode<T> node)
        {
            var currentValue = node.Value;
            while (node.Left != null)
            {
                currentValue = node.Left.Value;
                node = node.Left;
            }
            return currentValue;
        }

        // Обходы дерева
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversalRecursive(Root, action);
        }

        private void InOrderTraversalRecursive(BinaryTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left, action);
                action(node.Value);
                InOrderTraversalRecursive(node.Right, action);
            }
        }

        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversalRecursive(Root, action);
        }

        private void PreOrderTraversalRecursive(BinaryTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversalRecursive(node.Left, action);
                PreOrderTraversalRecursive(node.Right, action);
            }
        }

        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversalRecursive(Root, action);
        }

        private void PostOrderTraversalRecursive(BinaryTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                PostOrderTraversalRecursive(node.Left, action);
                PostOrderTraversalRecursive(node.Right, action);
                action(node.Value);
            }
        }
    }
}