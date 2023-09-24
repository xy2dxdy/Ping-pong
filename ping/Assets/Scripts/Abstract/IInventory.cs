using System;

public interface IInventory
{
    int capacity { get; set; }
    IInventoryItem GetItem(Type itemType);
    bool TryToAdd(IInventoryItem item);
    void Remove(Type itemType, int amount = 1);
    bool HasItem(Type type, out IInventoryItem item);
}
