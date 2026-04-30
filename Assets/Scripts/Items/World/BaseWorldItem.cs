using Fusion;
using FusionDemo;

public abstract class BaseWorldItem : NetworkBehaviour, IInteractable {
    public abstract void Interact(NetworkRunner runner, PlayerRef playerInteracting);
    public abstract Item GetItem();
    public abstract void SetVisual();
}
