using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRoom : RoomComponent
{
    [SerializeField] private float _ShieldActiveTime = 1f;
    [SerializeField] private float _CooldownTime = 10f;
    [SerializeField] private float _DamagedCooldownTime = 15f;
    private float _CurrentElapsed = 0f;
    private float _LastTime = 0f;
    private bool _ShieldActive = false;
    private bool _CooldownActive = false;

    void Update()
    {
        if(_ShieldActive || _CooldownActive)
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
            _CurrentElapsed += currentTime - _LastTime;
            if (_CurrentElapsed <= 0)
            {
                _CurrentElapsed = 0;
            }

            _LastTime = currentTime;
        }

        // Deactivates shield if the correct amount of seconds elapsed
        if(IsShieldActive() && _CurrentElapsed >= _ShieldActiveTime)
        {
            _ShieldActive = false;
            _CooldownActive = true;
        }

        // Deactivates cooldown if the correct amount of seconds elapsed based on the DAMAGE_STATE
        if(IsOnCooldown())
        {
            if ((damageState == DAMAGE_STATE.FUNCTIONAL && _CurrentElapsed >= _CooldownTime) ||
                    damageState == DAMAGE_STATE.DAMAGED && _CurrentElapsed >= _DamagedCooldownTime)
                _CooldownActive = false;
        }

    }

    // Overrides parent doAction method -> Activates the shield and cooldown
    protected override void doAction()
    {
        // Activate shield and all that
        if (!_ShieldActive && !_CooldownActive)
        {
            ActivateShield();
        }
    }

    // Starts the cooldown with the correct time
    private void ActivateShield()
    {
        _CurrentElapsed = 0f;
        _ShieldActive = true;
        _LastTime = Time.time;
    }

    // Returns true if shield is active
    public bool IsShieldActive()
    {
        return _ShieldActive;
    }

    // Returns true if cooldown is active
    public bool IsOnCooldown()
    {
        return _CooldownActive;
    }
}
