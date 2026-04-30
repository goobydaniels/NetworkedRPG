using Fusion;
using UnityEngine;
using static ItemDatabase;

public class Inventory : NetworkBehaviour {
    [Networked, Capacity(4)] private NetworkArray<int> Items => default;

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddItem(WorldItem item) {
        //items.Add(item.GetItem());
    }

    [ContextMenu("Debugging Use Item")]
    public void UseItem(/*int slot*/) {
        RPC_UseItem(0);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_UseItem(int slot) {
        //if (slot < 0 || slot >= items.Count) return;

        //Item item = items[slot];

        //if (item is Item<PotionInstance> potion) {
        //    var instance = potion.GetInstance();
        //    if (instance.Use(Runner, Object, 1)) {
        //        items.Remove(item);
        //    }
        //}

        //if (item is Item<SwordInstance> sword) {
        //    var instance = sword.GetInstance();
        //    //instance.Use(Runner, Object);
        //}
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
                    return;
                }
            }
        }
    }

    public void RemoveItemByID(int id) {
        if (HasStateAuthority) {
            for (int i = 0; i < Items.Length; i++) {
                if (Items[i] == id) {
                    Items.Set(id, 0);
                    return;
                }
            }
            Debug.LogWarning("Item ID does not exist within inventory");
        }
    }

    public void RemoveItemBySlot(int slot) {
        if (HasStateAuthority) {
            if (Items[slot] > 0 && Items.Length >= slot) {
                Items.Set(slot, 0);
                return;
            }
            Debug.LogWarning("Slot is already empty OR inventory isn't big enough");
        }
    }
}
