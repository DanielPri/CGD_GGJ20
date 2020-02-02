using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRoom : RoomComponent {
    [SerializeField] float maxSpeed; //Speeds and the different multipliers when hurt
    [SerializeField] float damagedMultiplier; 
    [SerializeField] float destroyedMultiplier;
    float currentSpeed;

    [SerializeField] float abilityMultiplier; //Speed boost multiplier

    // Update is called once per frame
    new void Update() {
        base.Update();

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
        if (abilityTimer >= abilityDuration && activated) {
            activated = false;
            abilityTimer = 0;
        }
        if (abilityTimer >= abilityCooldown) {
            onCooldown = false;
        }
    }

    protected override void doAction() {
        if (!activated) { // && Storage.energy >= energyCost
            activated = true;
            abilityTimer = 0;
            //Storage.energy -= energyCost;
        }
    }

    protected override void endAction() {
    }
}
