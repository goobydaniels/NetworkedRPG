using Fusion;
using System;

[Serializable]
public class SwordInstance : ItemInstance {
    public int currentDurability;
    public int useDurability;

    public SwordInstance(SwordItem source) {
        Instance = source;
        currentDurability = source.durability;
        useDurability = source.durabilityUse;
    }

    public void Use(NetworkRunner runner, NetworkObject user, NetworkObject target) {
        base.Use(runner, user, useDurability);
    }
}
