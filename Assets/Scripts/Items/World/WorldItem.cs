using Fusion;
using UnityEngine;

public class WorldItem : BaseWorldItem {
    [SerializeField] private Item item;

    public override void Spawned() {
        SetVisual();
    }

    public override void Interact(NetworkRunner runner, PlayerRef playerInteracting) {
        var player = runner.GetPlayerObject(playerInteracting);

        if (player.TryGetBehaviour<Inventory>(out var playerInventory)) {
            playerInventory.RPC_AddItem(this);
            Runner.Despawn(Object);
        }
    }

    public override void SetVisual() {
        gameObject.name = item.itemName;

        if (TryGetComponent<MeshFilter>(out var filter)) {
            filter.mesh = item.model;
        }

        if (TryGetComponent<Renderer>(out var renderer)) {
            renderer.material = item.material;
        }
    }

    public override Item GetItem() {
        return item;
    }
}
