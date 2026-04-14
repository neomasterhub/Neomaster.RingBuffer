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
    CheckData(buffer, defaultBuffer, [], defaultBuffer);
  }

  [Fact]
  public void Push()
  {
    var buffer = new RingBuffer<int>(3);

    buffer.Push(1);
    CheckData(buffer, [1, 0, 0], [1], [0, 0]);

    buffer.Push(2);
    CheckData(buffer, [1, 2, 0], [1, 2], [0]);

    buffer.Push(3);
    CheckData(buffer, [1, 2, 3], [], [1, 2, 3]);

    buffer.Push(4);
    CheckData(buffer, [4, 2, 3], [4], [2, 3]);
  }

  private static void CheckData(
    RingBuffer<int> buffer,
    int[] expectedBuffer,
    int[] expectedLeft,
    int[] expectedRight)
  {
    Assert.Equal(expectedBuffer, buffer.Buffer.ToArray());
    Assert.Equal(expectedLeft, buffer.Left.ToArray());
    Assert.Equal(expectedRight, buffer.Right.ToArray());
  }
}
