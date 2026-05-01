using Fusion;
using System.Linq;
using UnityEngine;
using static ItemDatabase;

public class Inventory : NetworkBehaviour {
    [Networked, Capacity(4), UnitySerializeField]
    private NetworkArray<int> Items { get; set; }

    public override void Spawned() {
        if (Object.HasInputAuthority) {
            Debug.Log($"Inventory loaded with items: {string.Join(",", Items.Select(x => x.ToString()))}");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddItem(WorldItem item) {
        var itemID = ItemDataBase.GetItemID(item.GetItem());
        AddItem(itemID);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_UseItem(int slot) {
        if (slot < 0 || slot >= Items.Length) return;

        Item item = GetItemAt(slot);

        if (item is Item<PotionInstance> potion) {
            var instance = potion.GetInstance();
            if (instance.Use(Runner, Object, 1)) {
                RemoveItemBySlot(slot);
            }
        }

        if (item is Item<SwordInstance> sword) {
            var instance = sword.GetInstance();
            //instance.Use(Runner, Object);
        }
    }

    public Item GetItemAt(int slot) {
        int id = Items[slot];
        return ItemDataBase.GetItem(id);
    }

    public void AddItem(int id) {
        if (HasStateAuthority) {
            for (int i = 0; i < Items.Length; i++) {
                if (Items[i] == 0) {
                    Items.Set(i, id);
                    Debug.Log("Added item at slot #" + i);
                    return;
                }
            }
        }
        Debug.LogWarning("Failed to add item to inventory");
    }

    public int RemoveItemByID(int id) {
        if (HasStateAuthority) {
            for (int i = 0; i < Items.Length; i++) {
                if (Items[i] == id) {
                    Items.Set(i, 0);
                    return i;
                }
            }
        }
        Debug.LogWarning("Item ID does not exist within inventory");
        return -1;
    }

    public int RemoveItemBySlot(int slot) {
        if (HasStateAuthority) {
            if (Items[slot] > 0 && Items.Length >= slot) {
                Items.Set(slot, 0);
                return slot ;
            }
        }
        Debug.LogWarning("Slot is already empty OR inventory isn't big enough");
        return -1;
    }

    public void SetInventory(NetworkArray<int> items) {
        Items = items; 
    }
}
