using System;

namespace Neomaster.RingBuffer;

public class RingBuffer<TItem>
{
  private readonly TItem[] _buffer;
  private int _head;

  public RingBuffer(int capacity)
  {
    if (capacity <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
    }

    _buffer = new TItem[capacity];
    Capacity = capacity;
  }

  public int Capacity { get; }

  public void Push(TItem item)
  {
    _buffer[_head] = item;

    _head++;
    if (_head == Capacity)
    {
      _head = 0;
    }
  }
}
