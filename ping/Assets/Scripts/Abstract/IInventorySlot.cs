using System;

public interface IInventorySlot
{
    bool isEmpty { get; }
    IInventoryItem item { get; }
    Type itemType { get; }
    int amount { get; }
    void SetItem(IInventoryItem item);
    void Clear();
}
