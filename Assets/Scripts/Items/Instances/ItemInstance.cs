using Fusion;
using System;
using UnityEngine;

[Serializable]
public abstract class ItemInstance {
    public Item Instance;
    public string id;

    public ItemInstance() {
        id = Guid.NewGuid().ToString();
    }

    public virtual bool Use(NetworkRunner runner, NetworkObject user, int quantity) {
        Instance.quantity = Mathf.Clamp(Instance.quantity - quantity, 0, Instance.maxQuantity);
        return Instance.quantity <= 0;
    }
}
