using System;
using UnityEngine;

public interface IInventoryItem
{
    Type type { get; }
    int amount { get; set; }
}
