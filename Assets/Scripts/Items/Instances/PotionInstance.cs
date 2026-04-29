using Fusion;
using System;
using UnityEngine;

[Serializable]
public class PotionInstance : ItemInstance {
    public PotionItem Instance;
    public StatusEffects effect;
    public int healAmount;
    public int hurtAmount;

    public PotionInstance(PotionItem source) {
        Instance = source;
        effect = source.statusEffect;
        healAmount = source.healAmount;
        hurtAmount = source.hurtAmount;
    }

    public override void Use(NetworkRunner runner, NetworkObject user) {
        if (!runner) return;

        if (user.TryGetComponent<Health>(out var userHealth)) {
            switch (effect) {
                case StatusEffects.HEAL:
                    Debug.Log("Heal");
                    userHealth.Heal(healAmount);
                    break;
                case StatusEffects.POISION:
                    Debug.Log("POISION");
                    userHealth.Hit(hurtAmount);
                    break;
            }
        } 
    }
}
