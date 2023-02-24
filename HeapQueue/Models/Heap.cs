namespace HeapQueue.Models;

public sealed record Heap(List<Node>? Nodes = null) : IEquatable<Heap>
{

    /// <summary>
    /// The nodes that make up the heap.
    /// </summary>
    public List<Node> Nodes { get; set; } = Nodes ?? new();

    /// <summary>
    /// Add a new node to the heap.
    /// </summary>
    /// <param name="node">The node to add.</param>
    public void Add(Node node) => Nodes.Add(node);

    /// <summary>
    /// Swap two nodes in the heap.
    /// </summary>
    /// <param name="index1">The index of the first node.</param>
    /// <param name="index2">The index of the second node.</param>
    public void Swap(int index1, int index2) => (Nodes[index1], Nodes[index2]) = (Nodes[index2], Nodes[index1]);

    /// <summary>
    /// Removes and returns the last node in the heap.
    /// </summary>
    /// <returns>The last node in the heap.</returns>
    public Node Pop() 
    {
        var node = Nodes[^1];
        Nodes.RemoveAt(Nodes.Count - 1);
        return node;
    }

    /// <summary>
    /// Represent the list of nodes in the heap.
    /// </summary>
    /// <returns>The list of nodes in the heap as a string.</returns>
    public override string ToString()
    {
        return $"Nodes: {string.Join(", ", Nodes)}";
    }

    /// <summary>
    /// Check if two heaps are equal.
    /// </summary>
    /// <param name="other">The other heap to compare to.</param>
    public bool Equals(Heap? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Nodes.SequenceEqual(other.Nodes);
    }

    /// <summary>
    /// Get the hash code of the heap.
    /// </summary>
    /// <returns>The hash code of the heap.</returns>
    public override int GetHashCode() => HashCode.Combine(Nodes);

}
