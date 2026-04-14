namespace Neomaster.RingBuffer.Tests;

public class RingBufferTests
{
  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void Create_InvalidCapacity(int capacity)
  {
    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RingBuffer<int>(capacity));
    Assert.Equal("capacity", ex.ParamName);
    Assert.Equal("Capacity must be greater than zero. (Parameter 'capacity')", ex.Message);
  }
}
