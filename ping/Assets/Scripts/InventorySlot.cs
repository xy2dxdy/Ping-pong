using System;

public class InventorySlot : IInventorySlot
{
    public bool isEmpty => item == null;
    public IInventoryItem item { get; private set; }
    public Type itemType => item.type;
    public int amount => isEmpty ? 0 : item.amount;
    public void SetItem(IInventoryItem item)
    {
        if (!isEmpty)
            return;
        this.item = item;
    }
    public void Clear()
    {
        if (isEmpty)
            return;
        item.amount = 0;
        item = null;
    }
}
