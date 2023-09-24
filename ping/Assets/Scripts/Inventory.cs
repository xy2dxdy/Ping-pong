using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class Inventory : IInventory
{
    public int capacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    private List<IInventorySlot> _slots;
    public Inventory(int capacity)
    {
        this.capacity = capacity;
        _slots = new List<IInventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot());
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }
    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }

    public void Remove(Type itemType, int amount = 1)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty
        && slot.itemType == itemType);
        if (slotWithSameItemButNotEmpty != null)
        {
            slotWithSameItemButNotEmpty.item.amount--;
            if (slotWithSameItemButNotEmpty.amount <= 0)
                slotWithSameItemButNotEmpty.Clear();
        }
        else
            return;
    }

    public bool TryToAdd(IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty
        && slot.itemType == item.type);
        if (slotWithSameItemButNotEmpty != null)
            return AddToSlot(slotWithSameItemButNotEmpty, item);
        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return AddToSlot(emptySlot, item);
        return false;
    }
    private bool AddToSlot(IInventorySlot slot, IInventoryItem item)
    {
        if (slot.isEmpty)
            slot.SetItem(item);
        else
            slot.item.amount++;
        return true;
    }

}
