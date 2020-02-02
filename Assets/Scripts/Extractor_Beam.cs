using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor_Beam : RoomComponent
{
    public static bool extracting;
    public static Vector3 extractor_beam_position;
    public bool isHealthy;
    Animator animator;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        extractor_beam_position = transform.position;
        extracting = false;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    new void Update()
    {
        if (damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            isHealthy = true;
            animator.speed = 1;
        }
        else if (damageState == DAMAGE_STATE.DAMAGED)
        {
            isHealthy = true;
            animator.speed = 0.5f;
        }
        else if (damageState == DAMAGE_STATE.DESTROYED)
        {
            isHealthy = false;
        }

        base.Update();
        animator.SetBool("isHealthy", isHealthy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Resource_Water")
        {
            Storage.resources_Water += 1;
        }
        if (other.tag == "Resource_Metal")
        {
            Storage.resources_Metal += 1;
        }
    }

    protected override void doAction()
    {
        // Activate extractor beam and all that
        // TODO: Check energy cost and if there is enough
        if (!activated && damageState != DAMAGE_STATE.DESTROYED && PowerPlantRoom.currentEnergy > 0.00f)
        {
            PowerPlantRoom.currentEnergy -= 0.01f * Time.deltaTime;
            extracting = true;
        }
    }

    protected override void endAction()
    {
        extracting = false;
    }
}
