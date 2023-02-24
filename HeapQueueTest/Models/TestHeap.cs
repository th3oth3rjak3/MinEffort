namespace HeapQueueTest.Models;

public class TestHeap
{
    [Fact]
    public void ItShouldContainNodes()
    {
        List<Node> nodes = new();
        Enumerable.Range(1, 10).ToList().ForEach(x => nodes.Add(new Node(x, new(0, 0))));
        Heap heap = new() { Nodes = nodes };
        Assert.Equal(nodes, heap.Nodes);
        Assert.Equal(nodes.Count, heap.Nodes.Count);
    }

    [Fact]
    public void ItShouldGetLongerAfterAddingNewNodes()
    {
        List<Node> nodes = new();
        Enumerable.Range(1, 10).ToList().ForEach(x => nodes.Add(new Node(x, new(0, 0))));
        Heap heap = new() { Nodes = nodes };
        heap.Add(new Node(11, new(0, 0)));
        Assert.Equal(11, heap.Nodes.Count);
    }
}
