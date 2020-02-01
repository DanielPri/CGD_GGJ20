using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRoom : RoomComponent
{
    [Range(0.0f, 1.0f)]
    public float oxygenRemaining = 1f;
    public float oxygenLeakSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        handleOxygenReserves();
        // handle room stuff like light turning on;
    }

    private void handleOxygenReserves()
    {
        if(damageState == DAMAGE_STATE.DESTROYED && oxygenRemaining > 0)
        {
            oxygenRemaining -= oxygenLeakSpeed * Time.deltaTime;
            if(oxygenRemaining < 0) { oxygenRemaining = 0; }
        }
        if(damageState == DAMAGE_STATE.FUNCTIONAL && oxygenRemaining < 1)
        {
            if (oxygenRemaining > 1) { oxygenRemaining = 1; }
        }
    }


}
