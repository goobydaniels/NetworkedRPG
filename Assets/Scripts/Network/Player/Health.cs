using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour {
    [SerializeField] private int startHealth = 5;

    [Networked, OnChangedRender(nameof(OnCurrentHealthChanged))]
    public int CurrentHealth { get; private set; }

    public bool IsAlive => CurrentHealth > 0;

    public override void Spawned() {
        if (HasStateAuthority) {
            CurrentHealth = startHealth;
        }
    }

    public override void Render() {
        NetworkBehaviourBufferInterpolator interp = new(this);
        bool isAlive = interp.Int(nameof(CurrentHealth)) > 0;

        // Do anything else below using isAlive 
    }

    public bool Hit(int damage) {
        if (!IsAlive) return false;

        CurrentHealth -= damage;

        if (!IsAlive) {
            CurrentHealth = 0;
            // Do anything else here
        }

        return true;
    }

    public bool Heal(int health) {
        if (!IsAlive) return false;

        CurrentHealth += Mathf.Clamp(health, 0, startHealth);

        return true;
    }

    public void Revive() {
        CurrentHealth = startHealth;
    }

    private void OnCurrentHealthChanged() {
        if (CurrentHealth <= 0) return;
    }
}
