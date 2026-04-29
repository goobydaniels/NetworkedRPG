using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion")]
public class PotionItem : Item<PotionInstance> {
    [Header("Potion Data")]
    public StatusEffects statusEffect;
    public int healAmount;
    public int hurtAmount;

    public override PotionInstance GetInstance() {
        return new PotionInstance(this);
    }
}
