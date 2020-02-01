using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairZone : MonoBehaviour
{
    public bool isOccupied { get; set; }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            isOccupied = true;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isOccupied = false;
        }
    }
}
