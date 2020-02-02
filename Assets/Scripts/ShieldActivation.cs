using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivation : RoomComponent
{
    [SerializeField] Sprite shieldOn;
    [SerializeField] Sprite shieldOff;

    new void Update()
    {
        if (transform.parent.GetComponent<RoomComponent>().activated)
        {
            GetComponent<SpriteRenderer>().sprite = shieldOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = shieldOff;
        }
    }
}
