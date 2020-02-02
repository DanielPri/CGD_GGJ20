using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRoom : RoomComponent
{
    [Range(0.0f, 1.0f)]
    public float oxygenRemaining = 1f;
    public float oxygenLeakSpeed = 0.05f;
    public float oxygenReplenishSpeed = 0.5f;

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        handleOxygenReserves();
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
            oxygenRemaining += oxygenReplenishSpeed * Time.deltaTime;
            if (oxygenRemaining > 1) { oxygenRemaining = 1; }
        }
    }
}
