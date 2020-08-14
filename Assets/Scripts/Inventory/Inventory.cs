using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private string[] inventoryItems = { };

    public string[] GetInventoryItems() {
        string[] items = inventoryItems;
        return items;
    }
}
