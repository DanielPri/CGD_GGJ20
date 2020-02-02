using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineWheel : RoomComponent
{
    public float rotationSpeed;

    new void Update()
    {
        if(transform.parent.parent.GetComponent<RoomComponent>().damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            rotationSpeed = 360;
        }
        else if (transform.parent.parent.GetComponent<RoomComponent>().damageState == DAMAGE_STATE.DAMAGED)
        {
            rotationSpeed = 180;
        }
        else if (transform.parent.parent.GetComponent<RoomComponent>().damageState == DAMAGE_STATE.DESTROYED)
        {
            rotationSpeed = 0;
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
