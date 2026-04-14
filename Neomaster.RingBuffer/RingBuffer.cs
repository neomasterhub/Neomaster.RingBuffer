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
  public ReadOnlySpan<TItem> Buffer => _buffer;
  public ReadOnlySpan<TItem> Left => _buffer.AsSpan(0, _head);
  public ReadOnlySpan<TItem> Right => _buffer.AsSpan(_head);

  public void Push(TItem item)
  {
    _buffer[_head] = item;
    _head = (_head + 1 == Capacity) ? 0 : _head + 1;
  }
}
