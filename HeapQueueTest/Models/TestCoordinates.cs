namespace HeapQueueTest.Models;
using HeapQueue.Models;

public class TestCoordinates
{
    [Fact]
    public void ItShouldCreateCoordinates()
    {
        Coordinates coordinates = new(1, 2);
        Coordinates expectedCoordinates = new(1, 2);
        Assert.Equal(1, coordinates.Row);
        Assert.Equal(2, coordinates.Column);
        Assert.Equal(expectedCoordinates, coordinates);
    }
}
