using Fusion;
using System;

[Serializable]
public abstract class ItemInstance {
    public string id;

    public ItemInstance() {
        id = Guid.NewGuid().ToString();
    }

    public abstract void Use(NetworkRunner runner, NetworkObject user);
}
