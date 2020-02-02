using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor_Beam : RoomComponent
{
    public static bool extracting;
    public static Vector3 extractor_beam_position;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        extractor_beam_position = transform.position;
        extracting = false;
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
