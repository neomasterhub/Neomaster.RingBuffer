using System;
using System.Collections.Generic;

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

  public bool Match(ReadOnlySpan<TItem> items, IEqualityComparer<TItem> comparer = null)
  {
    if (items.Length == 0 || items.Length > Capacity)
    {
      return false;
    }

    comparer ??= EqualityComparer<TItem>.Default;

    var j = _head == 0 ? Capacity - 1 : _head - 1;
    for (var i = items.Length - 1; i >= 0; i++)
    {
      if (!comparer.Equals(items[i], _buffer[j]))
      {
        return false;
      }

      j = j == 0 ? Capacity - 1 : j - 1;
    }

    return true;
  }
}
