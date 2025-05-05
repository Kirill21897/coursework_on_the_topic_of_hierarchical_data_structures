using System;
using System.Collections.Generic;

#nullable enable

public class TreeNodeIER
{
    public Guid Id { get; }
    public object Data { get; set; }
    public Type? TypeData => Data?.GetType();
    public List<TreeNodeIER> Children { get; private set; }
    public TreeNodeIER? Parent { get; private set; }

    public bool IsRoot => Parent == null;
    public bool IsLeaf => Children.Count == 0;

    public int Level
    {
        get
        {
            int level = 0;
            var current = Parent;
            while (current != null)
            {
                level++;
                current = current.Parent;
            }
            return level;
        }
    }

    private static readonly Dictionary<Guid, TreeNodeIER> s_nodesById = new();

    public TreeNodeIER? this[Guid id] => 
        s_nodesById.TryGetValue(id, out var node) ? node : null;

    public static TreeNodeIER? GetById(Guid id) => 
    s_nodesById.TryGetValue(id, out var node) ? node : null;

    public TreeNodeIER(object data)
    {
        Data = data;
        Id = Guid.NewGuid();
        Children = new List<TreeNodeIER>();
        Parent = null;
        s_nodesById.Add(Id, this);
    }

    public void AddChild(TreeNodeIER child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));

        child.Parent?.Children.Remove(child);

        child.Parent = this;
        Children.Add(child);
    }

    public void Remove()
    {
        if (Parent != null)
        {
            Parent.Children.Remove(this);
        }

        s_nodesById.Remove(Id);

        foreach (var child in Children.ToList())
        {
            child.Remove();
        }

        Children.Clear();
    }

    public TreeNodeIER? Find(Predicate<object> predicate)
    {
        if (predicate(Data)) return this;

        foreach (var child in Children)
        {
            var result = child.Find(predicate);
            if (result != null) return result;
        }

        return null;
    }

    public IEnumerable<TreeNodeIER> TraverseDepthFirst()
    {
        yield return this;

        foreach (var child in Children)
        {
            foreach (var descendant in child.TraverseDepthFirst())
            {
                yield return descendant;
            }
        }
    }

    public IEnumerable<TreeNodeIER> TraverseBreadthFirst()
    {
        Queue<TreeNodeIER> queue = new();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            yield return node;

            foreach (var child in node.Children)
            {
                queue.Enqueue(child);
            }
        }
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + $"[{Id}] {Data}");
        foreach (var child in Children)
        {
            child.Print(indent + 2);
        }
    }
}