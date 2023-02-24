namespace HeapQueueTest;

public class MinHeapTests
{
    [Fact]
    public void ItShouldGetLongerAfterEnqueuing()
    {
        Node node = new(1, new(1, 2));
        MinHeap minHeap = new();
        minHeap.Enqueue(node);
        Assert.Equal(1, minHeap.Size);
    }

    [Theory]
    [InlineData(new[] { 3, 2, 1 }, new[] { 1, 3, 2 })]
    public void ItShouldAdjustPriorityBasedOnTheNodeValue(int[] inputValues, int[] resultOrder)
    {
        List<Node> nodes = inputValues.ToList().Select(x => new Node(x, new(0, 0))).ToList();
        List<Node> expectedNodes = resultOrder.ToList().Select(x => new Node(x, new(0, 0))).ToList();
        MinHeap expectedMinHeap = MinHeap.Of(expectedNodes);
        MinHeap minHeap = new();
        foreach (Node node in nodes)
        {
            minHeap.Enqueue(node);
        }
        Assert.Equal(expectedMinHeap, minHeap);
    }

    [Theory]
    [InlineData(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
    public void ItShouldHeapifyAfterOfIsCalled(int[] inputValues, int[] resultOrder)
    {
        List<Node> nodes = inputValues.ToList().Select(x => new Node(x, new(0, 0))).ToList();
        List<Node> expectedNodes = resultOrder.ToList().Select(x => new Node(x, new(0, 0))).ToList();
        MinHeap expectedMinHeap = new()
        {
            Heap = new() { Nodes = expectedNodes },
            Size = expectedNodes.Count
        };
        MinHeap minHeap = MinHeap.Of(nodes);
        Assert.Equal(expectedMinHeap, minHeap);
    }

    [Fact]
    public void ItShouldGetSmallerWhenDequeuing()
    {
        Node node = new(1, new(1, 2));
        MinHeap minHeap = new();
        minHeap.Enqueue(node);
        var popped = minHeap.Dequeue();
        Assert.Equal(node, popped);
        Assert.Equal(0, minHeap.Size);
    }
}
