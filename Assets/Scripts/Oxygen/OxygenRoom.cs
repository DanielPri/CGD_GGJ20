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

    private Animator animator;

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        handleOxygenReserves();
        if (damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            animator.SetTrigger("isFull");
        }
        else if (damageState == DAMAGE_STATE.DAMAGED)
        {
            animator.SetTrigger("isMid");
        }
        else
        {
            animator.SetTrigger("isLow");
        }
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
