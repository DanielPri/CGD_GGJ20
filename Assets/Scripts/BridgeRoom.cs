using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRoom : RoomComponent
{
    public bool isPiloting = false;
    public bool isEngineFunctional = true;

    float time = 0;

    new void Start()
    {
        base.Start();
        spriteRenderer = null; //since bridge doesnt have one
    }

    new void Update()
    {
        if (isPiloting)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                if (PowerPlantRoom.currentEnergy >= 5)
                {
                    PowerPlantRoom.currentEnergy -= 5;
                }
                time = 0;
            }
        }
        base.Update();
    }
    protected override void doAction()
    {
        if (isEngineFunctional)
        {
            isPiloting = true;
        }
    }

    protected override void endAction()
    {
        isPiloting = false;
    }

    /*new void repair()
    {
        // do nothing
    }*/
}
