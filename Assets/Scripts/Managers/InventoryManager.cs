using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }
    public List<AllItems> inventoryItems = new List<AllItems>();


    private void Awake()
    {
        Instance = this;
    }

    public void AddItems(AllItems item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }
    }

    public void RemoveItems(AllItems item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
    }

    public enum AllItems
    {
        RedKey,
        BlueKey,
    }
}
