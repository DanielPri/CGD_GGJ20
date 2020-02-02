using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor_Beam : RoomComponent
{
    public static bool extracting;
    public static Vector3 extractor_beam_position;
    public bool isHealthy;
    Animator animator;
    float time;

    // Start is called before the first frame update
    new void Start()
    {
        time = 0;
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

        if (extracting)
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
    }

    protected override void doAction()
    {
        // Activate extractor beam and all that
        // TODO: Check energy cost and if there is enough
        if (!activated && damageState != DAMAGE_STATE.DESTROYED && PowerPlantRoom.currentEnergy > 0.00f)
        {
            extracting = true;
        }
    }

    protected override void endAction()
    {
        extracting = false;
    }
}
