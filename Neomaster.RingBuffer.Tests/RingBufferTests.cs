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

  [Fact]
  public void Push()
  {
    var buffer = new RingBuffer<int>(3);

    buffer.Push(1);
    Assert.Equal([1, 0, 0], buffer.Buffer.ToArray());
    Assert.Equal([1], buffer.Left.ToArray());
    Assert.Equal([0, 0], buffer.Right.ToArray());

    buffer.Push(2);
    Assert.Equal([1, 2, 0], buffer.Buffer.ToArray());
    Assert.Equal([1, 2], buffer.Left.ToArray());
    Assert.Equal([0], buffer.Right.ToArray());

    buffer.Push(3);
    Assert.Equal([1, 2, 3], buffer.Buffer.ToArray());
    Assert.Equal([], buffer.Left.ToArray());
    Assert.Equal([1, 2, 3], buffer.Right.ToArray());

    buffer.Push(4);
    Assert.Equal([4, 2, 3], buffer.Buffer.ToArray());
    Assert.Equal([4], buffer.Left.ToArray());
    Assert.Equal([2, 3], buffer.Right.ToArray());
  }
}
