using Fusion;
using System;

[Serializable]
public class SwordInstance : ItemInstance {
    public SwordItem Instance;
    public int currentDurability;
    public int useDurability;

    public SwordInstance(SwordItem source) {
        Instance = source;
        currentDurability = source.durability;
        useDurability = source.durabilityUse;
    }

    public override void Use(NetworkRunner runner, NetworkObject user) {
        currentDurability -= useDurability;
    }
}
