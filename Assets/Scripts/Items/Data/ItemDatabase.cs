using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/Database")]
public class ItemDatabase : ScriptableObject {
    private static ItemDatabase singleton;
    public static ItemDatabase ItemDataBase {
        get {
            if (singleton == null) {
                singleton = Resources.Load<ItemDatabase>("ItemDatabase");
                if (singleton == null) {
                    Debug.LogError("Critical: ItemDatabase not found at Resources/ItemDatabase!");
                }
            }
            return singleton;
        }
    }

    [SerializeField] private List<Item> Items = new();
    private Dictionary<int, Item> itemLookup;

    [SerializeField] private int tempID;

    public void Init() {
        Items ??= new();
        itemLookup = Items.ToDictionary(item => item.id, item => item);
    }

    public Item GetItem(int id) {
        if (itemLookup == null) { Init(); }
        return itemLookup.TryGetValue(id, out var item) ? item : null;
    }

    public int GetItemID(Item item) {
        if (itemLookup == null) { Init(); }

        foreach (var entry in itemLookup) {
            if (entry.Value.Equals(item)) {
                return entry.Key;
            }
        }
        return -1;
    }

    [ContextMenu("Debugging, Get Item At Slot")]
    public void PrintItemAtSlot() {
        Item item = GetItem(tempID);
        if (item != null) {
            Debug.Log("Item Found: " + item.itemName);
        } else {
           Debug.LogWarning("No item by this id available");  
        }
    }
}
