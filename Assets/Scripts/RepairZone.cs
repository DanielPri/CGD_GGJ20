using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairZone : MonoBehaviour
{
    public bool isOccupied { get; set; }
    public PLAYER occupyingPlayer = PLAYER.NONE;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            isOccupied = true;

            if (occupyingPlayer == PLAYER.NONE)
            {
                //occupyingPlayer = col.gameObject.GetComponent<PlayerScript>().player;
            }
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isOccupied = false;
            occupyingPlayer = PLAYER.NONE;
        }
    }
}
