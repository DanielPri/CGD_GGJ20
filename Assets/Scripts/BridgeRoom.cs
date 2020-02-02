using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRoom : RoomComponent
{
    public bool isPiloting = false;
    public bool isEngineFunctional = true;

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

    new void repair()
    {
        // do nothing
    }
}
