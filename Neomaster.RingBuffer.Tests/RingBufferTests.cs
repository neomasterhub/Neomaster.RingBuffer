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

  [Fact]
  public void Create_ValidCapacity()
  {
    const int capacity = 10;
    var defaultBuffer = new int[capacity];

    var buffer = new RingBuffer<int>(capacity);

    Assert.Equal(capacity, buffer.Capacity);
    Assert.Equal(defaultBuffer, buffer.Buffer.ToArray());
    Assert.Equal([], buffer.Left.ToArray());
    Assert.Equal(defaultBuffer, buffer.Right.ToArray());
  }
}
