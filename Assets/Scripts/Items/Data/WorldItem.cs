using Fusion;
using FusionDemo;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable {
    [SerializeField] private Item item;

    private void Awake() {
        gameObject.name = item.itemName;
        Instantiate(item.model, transform);
    }

    public void Interact(NetworkRunner runner, PlayerRef playerRef) {
        NetworkObject player = runner.GetPlayerObject(playerRef);

    }
}
