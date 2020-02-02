using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldRoom : RoomComponent
{
    [SerializeField] private float _DamagedCooldownModifier = 5f;
    private float _LastTime = 0f;

    new void Update()
    {
        // Update from RoomComponent
        base.Update();

        if (activated || onCooldown)
        {
            HandleTimeElapsed();
        }
    }

    // Do something in the room when player enters maybe
    //private void OnTriggerEnter2D(Collider2D col)
    //{
      //  if(col.tag == "Player")
      // {
            // Do something in the room when player enters
      //  }
    //}

    private void HandleTimeElapsed()
    {
        // Get current time
        float currentTime = Time.time;

        // Count one second
        if(currentTime - _LastTime >= 1)
        {
            abilityTimer += 1;
            _LastTime = currentTime;

            // Deactivates shield if the correct amount of seconds elapsed
            if (activated && abilityTimer >= abilityDuration)
            {
                activated = false;
                isShielded = false;
                onCooldown = true;
            }

            // Deactivates cooldown if the correct amount of seconds elapsed based on the DAMAGE_STATE
            if (onCooldown)
            {
                if ((damageState == DAMAGE_STATE.FUNCTIONAL && abilityTimer >= (abilityCooldown + abilityDuration)) ||
                        (damageState == DAMAGE_STATE.DAMAGED && abilityTimer >= (abilityCooldown + _DamagedCooldownModifier + abilityDuration)))
                    onCooldown = false;
            }
        }
    }

    // Overrides parent doAction method -> Activates the shield and cooldown
    protected override void doAction()
    {
        // Activate shield and all that
        // TODO: Check energy cost and if there is enough
        if (!activated && !onCooldown && damageState != DAMAGE_STATE.DESTROYED
            && (PowerPlantRoom.currentEnergy >= energyCost))
        {
            ActivateShield();
        }
    }

    // Starts the cooldown with the correct time
    private void ActivateShield()
    {
        abilityTimer = 0;
        activated = true;
        isShielded = true;
        _LastTime = Time.time;
    }
}
