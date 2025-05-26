using System;
using System.Collections.Generic;
using System.Linq;

public static class Tree
{
    private static readonly Dictionary<Guid, Node> s_nodesById = new();

    public static void AddNode(Node node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        s_nodesById[node.Id] = node;
    }

    public static void RemoveNode(Guid id)
    {
        s_nodesById.Remove(id);
    }

    public static Node? GetById(Guid id)
    {
        return s_nodesById.TryGetValue(id, out var node) ? node : null;
    }

    // Создание корневого узла
    public static Node CreateRoot(object data)
    {
        return new Node(data);
    }

    // Получить все узлы дерева
    public static IReadOnlyCollection<Node> GetAllNodes()
    {
        return s_nodesById.Values.ToList();
    }

    // Найти все узлы, удовлетворяющие предикату
    public static List<Node> FindAll(Predicate<object> predicate)
    {
        return s_nodesById.Values
            .Where(n => predicate(n.Data))
            .ToList();
    }

    // Подсчёт общего количества узлов
    public static int CountNodes()
    {
        return s_nodesById.Count;
    }

    // Проверяет, существует ли цикл в дереве
    public static bool HasCycle(Node startNode)
    {
        var visited = new HashSet<Node>();
        var current = startNode.Parent;

        while (current != null)
        {
            if (visited.Contains(current))
                return true;

            visited.Add(current);
            current = current.Parent;
        }

        return false;
    }

    // Удалить всё дерево
    public static void Clear()
    {
        foreach (var node in s_nodesById.Values.ToList())
        {
            node.Remove();
        }
        s_nodesById.Clear();
    }

    // Вывести структуру всего дерева
    public static void PrintAllTrees()
    {
        var roots = s_nodesById.Values.Where(n => n.IsRoot).ToList();

        if (roots.Count == 0)
        {
            Console.WriteLine("Деревья отсутствуют.");
            return;
        }

        foreach (var root in roots)
        {
            Console.WriteLine($"Корень: {root.Id}");
            root.Print(2);
        }
    }

    // Клонировать дерево
    public static Node CloneSubtree(Node root)
    {
        var oldToNewMap = new Dictionary<Guid, Node>();

        Node CloneNode(Node node)
        {
            var newNode = new Node(node.Data);
            oldToNewMap[node.Id] = newNode;
            return newNode;
        }

        var queue = new Queue<Node>();
        var newRoot = CloneNode(root);
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var newCurrent = oldToNewMap[current.Id];

            foreach (var child in current.Children)
            {
                var newChild = CloneNode(child);
                newCurrent.AddChild(newChild);
                queue.Enqueue(child);
            }
        }

        return newRoot;
    }
}