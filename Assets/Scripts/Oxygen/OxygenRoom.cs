using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRoom : MonoBehaviour
{
    public DAMAGE_STATE damageState = DAMAGE_STATE.FUNCTIONAL;
    public float repairSpeed = 2f;
    [Range(0.0f, 1.0f)]
    public float oxygenRemaining = 1f;
    public float oxygenLeakSpeed = 2f;

    private RepairZone repairZone;
    private bool canRepair = false;
    private float repairProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        repairZone = GetComponentInChildren<RepairZone>();
    }

    // Update is called once per frame
    void Update()
    {
        setRepairStatus();
        handleInput();
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

    void handleInput()
    {
        //NOTE this will activate regardless of which player clicks the repair button but we'll worry about that later
        //ALSO the button names are placeholders until we decide on the button
        if (Input.GetButton("Player1RepairButton") || Input.GetButton("Player2RepairButton"))
        {
            repair();
        }
    }

    void repair()
    {
        if (canRepair)
        {
            repairProgress += repairSpeed * Time.deltaTime;
            if (repairProgress >= 1f)
            {
                changeDamageState();
                repairProgress = 0f;
            }
        }
    }

    void changeDamageState()
    {
        if(damageState == DAMAGE_STATE.DESTROYED)
        {
            damageState = DAMAGE_STATE.DAMAGED;
        }
        else if (damageState == DAMAGE_STATE.DAMAGED)
        {
            damageState = DAMAGE_STATE.FUNCTIONAL;
        }
        else
        {
            Debug.Log("HEY WHAT ARE YOU DOING HERE THIS SHOULDNT BE POSSIBLE");
        }
    }

    void setRepairStatus()
    {
        if(damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            canRepair = false;
        }
        else {
            canRepair = repairZone.isOccupied;
        }
        
    }
}
