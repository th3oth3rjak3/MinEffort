namespace HeapQueueTest.Models;
using HeapQueue.Models;

public class TestNode
{
    [Fact]
    public void ItShouldCreateANode()
    {
        Node node = new(1, new(2, 3));
        Node node2 = new(1, new(2, 3));
        Assert.Equal(1, node.Value);
        Assert.Equal(2, node.Coordinates.Row);
        Assert.Equal(3, node.Coordinates.Column);
        Assert.Equal(node, node2);
    }
}
