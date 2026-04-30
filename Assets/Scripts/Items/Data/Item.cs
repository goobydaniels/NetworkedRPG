using UnityEngine;

// Non generic item class 
public abstract class Item : ScriptableObject {
    [Header("Default Item Data")]
    public int id;
    public string itemName;
    public ItemType type;
    public ItemRarity rarity;
    public int quantity = 1;
    public int maxQuantity = 1;

    [Header("Item Visualization")]
    public Sprite icon;
    public Mesh model;
    public Material material;
}

// Creating a generic Item class
public abstract class Item<T> : Item {
    public abstract T GetInstance();
}
