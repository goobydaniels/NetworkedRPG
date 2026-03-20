using UnityEngine;

public abstract class Item<T> : ScriptableObject {
    [Header("Default Item Data")]
    public string itemName;
    public ItemType type;
    public ItemRarity rarity;
    
    public abstract T GetInstance();
}
