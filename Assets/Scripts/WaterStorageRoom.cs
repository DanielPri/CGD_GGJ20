using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStorageRoom : RoomComponent
{
    private Animator animator;
    int maximum_water = 100;
    public float waterLeakSpeed = 1f;
    public static float resources_Water;

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        resources_Water = 0;
    }

    new void Update()
    {
        base.Update();
        if(resources_Water > maximum_water * 0.8f)
        {
            animator.SetTrigger("isFull");
        }
        else if (resources_Water < maximum_water * 0.1f)
        {
            animator.SetTrigger("isMid");
        }
        else
        {
            animator.SetTrigger("isEmpty");
        }
    }

    private void handleOxygenReserves()
    {
        if (damageState == DAMAGE_STATE.DESTROYED && resources_Water > 0)
        {
            resources_Water -= waterLeakSpeed * Time.deltaTime;
            if (resources_Water < 0) { resources_Water = 0; }
        }
    }
}
