using UnityEngine;
using System.Collections;

public class Storage : RoomComponent
{

    public static float resources_Metal;
    public float metalLeakSpeed = 1f;
    int maximum_storage = 100;

    new void Start()
    {
        base.Start();
        resources_Metal = 0;
    }

    private void handleOxygenReserves()
    {
        if (damageState == DAMAGE_STATE.DESTROYED && resources_Metal > 0)
        {
            resources_Metal -= metalLeakSpeed * Time.deltaTime;
            if (resources_Metal < 0) { resources_Metal = 0; }
        }
    }
}
