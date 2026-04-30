using Fusion;
using System;
using UnityEngine;

[Serializable]
public class PotionInstance : ItemInstance {
    public StatusEffects effect;
    public int healAmount;
    public int hurtAmount;

    public PotionInstance(PotionItem source) {
        Instance = source;
        effect = source.statusEffect;
        healAmount = source.healAmount;
        hurtAmount = source.hurtAmount;
    }

    public override bool Use(NetworkRunner runner, NetworkObject user, int quantity) {
        if (!runner) return false;

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

        return base.Use(runner, user, quantity);
    }
}
