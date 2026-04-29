using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : NetworkBehaviour {
    //[Networked, Capacity(4)] private NetworkArray<Item> InventoryItems => default;
    [SerializeField] private List<Item> items = new();

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddItem(WorldItem item) {
        items.Add(item.GetItem());
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_UseItem(int slot) {
        if (slot < 0 || slot >= items.Count) return;

        Item item = items[slot];

        if (item is Item<PotionInstance> potion) {
            PotionInstance instance = potion.GetInstance();
            instance.Use(Runner, Object);
            
            // After using item, remove it
            items.Remove(item);
        }
    }
}
