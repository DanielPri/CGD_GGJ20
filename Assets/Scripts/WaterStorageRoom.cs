using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStorageRoom : RoomComponent
{
    private Animator animator;
    int maximum_water = 100;

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    new void Update()
    {
        base.Update();
        if(Storage.resources_Water > maximum_water * 0.8f)
        {
            animator.SetTrigger("isFull");
        }
        else if (Storage.resources_Water < maximum_water * 0.1f)
        {
            animator.SetTrigger("isMid");
        }
        else
        {
            animator.SetTrigger("isEmpty");
        }
    }
}
