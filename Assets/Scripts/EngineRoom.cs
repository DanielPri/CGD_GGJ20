using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRoom : MonoBehaviour {
    [SerializeField] public DAMAGE_STATE damageState { get; private set; } = DAMAGE_STATE.FUNCTIONAL;
    [SerializeField] float maxSpeed; //Speeds and the different multipliers when hurt
    [SerializeField] float damagedMultiplier; 
    [SerializeField] float destroyedMultiplier;
    float currentSpeed;

    float abilityTimer = 0; //Calculates the duration and the cooldown
    public float energyCost { get; private set; }
    [HideInInspector] public bool activated { get; private set; } = false;
    [HideInInspector] public bool onCooldown { get; private set; } = false;
    [SerializeField] float abilityMultiplier; //Speed boost multiplier
    [SerializeField] float abilityCooldown;
    [SerializeField] float abilityDuration;

    // Update is called once per frame
    void Update() {
        if (abilityTimer < abilityCooldown) abilityTimer += Time.deltaTime;

        if (damageState == DAMAGE_STATE.FUNCTIONAL) {
            currentSpeed = maxSpeed;
        } else if (damageState == DAMAGE_STATE.DAMAGED) {
            currentSpeed = maxSpeed * damagedMultiplier;
        } else {
            currentSpeed = maxSpeed * destroyedMultiplier;
        }

        if (activated) {
            currentSpeed *= abilityMultiplier;
        }
        if (abilityTimer >= abilityDuration) {
            activated = false;
            abilityTimer = 0;
        }
        if (abilityTimer >= abilityCooldown) {
            onCooldown = false;
        }
    }

    public void getHit() {
        damageState++;
    }

    public void getRepaired() {
        damageState--;
    }

    public void activateAbility() { //Controller should look for the cooldown
        activated = true;
        abilityTimer = 0;
    }
}
