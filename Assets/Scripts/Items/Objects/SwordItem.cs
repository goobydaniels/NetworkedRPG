using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Items/Sword")]
public class SwordItem : Item<SwordInstance> {
    [Header("Sword Data")]
    public int damage; 
    public int durability;
    public int durabilityUse;
    public StatusEffects statusEffect;

    public override SwordInstance GetInstance() {
        return new SwordInstance(this);
    }
}
