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

  [Fact]
  public void StartsWith()
  {
    var buffer = new RingBuffer<int>(3);
    Assert.False(buffer.StartsWith([1]));
    Assert.False(buffer.StartsWith([0, 0, 0, 0]));
    Assert.True(buffer.StartsWith([]));
    Assert.True(buffer.StartsWith([0]));
    Assert.True(buffer.StartsWith([0, 0]));
    Assert.True(buffer.StartsWith([0, 0, 0]));

    buffer.Push(1);
    Assert.True(buffer.StartsWith([0]));
    Assert.True(buffer.StartsWith([0, 0]));
    Assert.True(buffer.StartsWith([0, 0, 1]));

    buffer.Push(2);
    Assert.True(buffer.StartsWith([0]));
    Assert.True(buffer.StartsWith([0, 1]));
    Assert.True(buffer.StartsWith([0, 1, 2]));

    buffer.Push(3);
    Assert.True(buffer.StartsWith([1]));
    Assert.True(buffer.StartsWith([1, 2]));
    Assert.True(buffer.StartsWith([1, 2, 3]));

    buffer.Push(4);
    Assert.True(buffer.StartsWith([2]));
    Assert.True(buffer.StartsWith([2, 3]));
    Assert.True(buffer.StartsWith([2, 3, 4]));
  }

  [Fact]
  public void EndsWith()
  {
    var buffer = new RingBuffer<int>(3);
    Assert.False(buffer.EndsWith([1]));
    Assert.False(buffer.EndsWith([0, 0, 0, 0]));
    Assert.True(buffer.EndsWith([]));
    Assert.True(buffer.EndsWith([0]));
    Assert.True(buffer.EndsWith([0, 0]));
    Assert.True(buffer.EndsWith([0, 0, 0]));

    buffer.Push(1);
    Assert.True(buffer.EndsWith([1]));
    Assert.True(buffer.EndsWith([0, 1]));
    Assert.True(buffer.EndsWith([0, 0, 1]));

    buffer.Push(2);
    Assert.True(buffer.EndsWith([2]));
    Assert.True(buffer.EndsWith([1, 2]));
    Assert.True(buffer.EndsWith([0, 1, 2]));

    buffer.Push(3);
    Assert.True(buffer.EndsWith([3]));
    Assert.True(buffer.EndsWith([2, 3]));
    Assert.True(buffer.EndsWith([1, 2, 3]));

    buffer.Push(4);
    Assert.True(buffer.EndsWith([4]));
    Assert.True(buffer.EndsWith([3, 4]));
    Assert.True(buffer.EndsWith([2, 3, 4]));
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
