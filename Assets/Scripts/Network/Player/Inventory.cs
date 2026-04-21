using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : NetworkBehaviour {
    //[Networked, Capacity(4)] private NetworkArray<Item> InventoryItems => default;
    [SerializeField] private List<Item> items = new();

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddItem(WorldItem item) {
        Debug.Log("Adding item: " + item.GetItem().itemName + " to inventory at slot: " + 0);
        items.Add(item.GetItem());
    }
}
