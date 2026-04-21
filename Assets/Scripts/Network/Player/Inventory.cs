using Fusion;
using UnityEngine;

public class Inventory : NetworkBehaviour {
    //[Networked, Capacity(10)] private NetworkLinkedList<Item> items => default;

    public override void Spawned() {
        if (Object.HasInputAuthority) {

        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddItem() {
        //items.Add(itemToAdd);
    }
}
