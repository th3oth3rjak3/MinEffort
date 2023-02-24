using HeapQueue.Models;

namespace HeapQueue;

/// <summary>
/// A min heap which is a complete binary tree that satisfies the heap property.
/// </summary>
public class MinHeap : IEquatable<MinHeap>
{
    /// <summary>
    /// The heap is a binary tree that is represented as an array.
    /// </summary>
    public Heap Heap { get; set; }

    /// <summary>
    /// The size of the heap.
    /// </summary>
    public int Size { get; set; }

    public MinHeap()
    {
        Heap = new();
        Size = 0;
    }

    /// <summary>
    /// Inserts a node into the heap and maintains the heap property.
    /// </summary>
    public void Enqueue(Node node)
    {
        Heap.Add(node);
        Size++;
        HeapifyUp(Size - 1);
    }

    /// <summary>
    /// Removes and returns the node with the minimum value from the heap
    /// and maintains the heap property.
    /// </summary>
    /// <returns>The node with the minimum value.</returns>
    public Node Dequeue()
    {
        Swap(0, Size - 1);
        Size--;
        HeapifyDown(0);
        return Heap.Pop();
    }

    /// <summary>
    /// Maintains the heap property by swapping the current node with its 
    /// parent if the parent's value is greater than the current node's value.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    private void HeapifyUp(int index)
    {
        int idx = index;
        while (ShouldSwapParent(idx))
        {
            SwapParent(idx);
            idx = ParentIndex(idx);
        }
    }

    /// <summary>
    /// Restore the heap property by swapping the current node with its
    /// smallest child node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    private void HeapifyDown(int index)
    {
        int idx = index;
        while (HasLeftChild(idx, Size))
        {
            if (ShouldSwapRight(idx))
            {
                SwapRight(idx);
                idx = RightChildIndex(idx);
            }
            else if (ShouldSwapLeft(idx))
            {
                SwapLeft(idx);
                idx = LeftChildIndex(idx);
            }
            else break;
        }
    }

    /// <summary>
    /// Restore the heap property for an entire heap.
    /// </summary>
    public void Heapify()
    {
        for (int i = Size - 1; i >= 0; i--)
        {
            HeapifyUp(i);
        }
    }

    /// <summary>
    /// Swaps the nodes at the given indices.
    /// </summary>
    /// <param name="index1">The index of the first node.</param>
    /// <param name="index2">The index of the second node.</param>
    private void Swap(int index1, int index2) =>
        Heap.Swap(index1, index2);


    /// <summary>
    /// Returns true if the right child node is less than the parent and left child node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>True if the right child node is less than the parent and left child node.</returns>
    private bool ShouldSwapRight(int index) =>
        HasRightChild(index, Size)
        && ValueAt(RightChildIndex(index)) < ValueAt(index)
        && ValueAt(RightChildIndex(index)) < ValueAt(LeftChildIndex(index));

    /// <summary>
    /// Checks to see if the left child node is less than the parent node.
    /// This should be checked after ShouldSwapRight.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>True if the left child node is less than the parent node.</returns>
    private bool ShouldSwapLeft(int index) =>
        HasLeftChild(index, Size)
        && ValueAt(LeftChildIndex(index)) < ValueAt(index);

    /// <summary>
    /// Checks to see if the parent node is greater than the current node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>True if the parent node is greater than the current node.</returns>
    private bool ShouldSwapParent(int index) =>
        HasParent(index, Size)
        && ValueAt(ParentIndex(index)) > ValueAt(index);

    /// <summary>
    /// Swap the right child node with the parent node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    private void SwapRight(int index) =>
        Swap(index, RightChildIndex(index));


    /// <summary>
    /// Swap the left child node with the parent node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    private void SwapLeft(int index) =>
        Swap(index, LeftChildIndex(index));

    /// <summary>
    /// Swap the parent node with the current node.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    private void SwapParent(int index) =>
        Swap(index, ParentIndex(index));

    /// <summary>
    /// Returns the value of the node at the given index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The value of the node at the given index.</returns>
    private int ValueAt(int index) => Heap.Nodes[index].Value;


    /// <summary>
    /// Returns the node at the given index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The node at the given index.</returns>
    private Node CurrentNode(int index) => Heap.Nodes[index];

    /// <summary>
    /// Returns the parent node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The parent node.</returns>
    private Node Parent(int index) => Heap.Nodes[ParentIndex(index)];

    /// <summary>
    /// Returns the left child node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The left child node.</returns>
    private Node LeftChild(int index) => Heap.Nodes[LeftChildIndex(index)];

    /// <summary>
    /// Returns the right child node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The right child node.</returns>
    private Node RightChild(int index) => Heap.Nodes[RightChildIndex(index)];

    /// <summary>
    /// Returns the index of the parent node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The index of the parent node.</returns>
    private static int ParentIndex(int index) => ((index + 1) / 2) - 1;

    /// <summary>
    /// Returns the left child node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The index of the left child node.</returns>
    private static int LeftChildIndex(int index) => ((index + 1) * 2) - 1;

    /// <summary>
    /// Returns the right child node given the current index.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <returns>The index of the right child node.</returns>
    private static int RightChildIndex(int index) => ((index + 1) * 2);

    /// <summary>
    /// Find out if the node at the current index can have a parent.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <param name="heapSize">The size of the heap.</param>
    /// <returns>True if the current node can have a parent, otherwise False.</returns>
    private static bool HasParent(int index, int heapSize) =>
        index > 0 && index < heapSize;

    /// <summary>
    /// Find out if the node at the current index can have a left child.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <param name="heapSize">The size of the heap.</param>
    /// <returns>True if the current node can have a left child, otherwise False.</returns>
    private static bool HasLeftChild(int index, int heapSize) =>
        LeftChildIndex(index) < heapSize;

    /// <summary>
    /// Find out if the node at the current index can have a right child.
    /// </summary>
    /// <param name="index">The index of the current node.</param>
    /// <param name="heapSize">The size of the heap.</param>
    /// <returns>True if the current node can have a right child, otherwise False.</returns>
    private static bool HasRightChild(int index, int heapSize) =>
        RightChildIndex(index) < heapSize;


    /// <summary>
    /// Creates a new MinHeap from the given nodes without maintaining the heap property.
    /// </summary>
    public static MinHeap Of(IEnumerable<Node> nodes)
    {
        MinHeap heap = new()
        {
            Heap = new(nodes.ToList()),
            Size = nodes.Count()
        };
        heap.Heapify();
        return heap;
    }

    /// <summary>
    /// Verify that this MinHeap is equal to another MinHeap.
    /// </summary>
    /// <param name="other">The other MinHeap to compare to.</param>
    /// <returns>True if the MinHeaps are equal, otherwise False.</returns>
    public bool Equals(MinHeap? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Size == other.Size && Heap.Equals(other.Heap);
    }

    /// <summary>
    /// Verify that this MinHeap is equal to another MinHeap.
    /// </summary>
    /// <param name="obj">The other MinHeap to compare to.</param>
    /// <returns>True if the MinHeaps are equal, otherwise False.</returns>
    public override bool Equals(object? obj) =>
        Equals(obj as MinHeap);

    /// <summary>
    /// Get the hash code for this MinHeap.
    /// </summary>
    /// <returns>The hash code for this MinHeap.</returns>
    public override int GetHashCode() =>
        HashCode.Combine(Heap, Size);

}
