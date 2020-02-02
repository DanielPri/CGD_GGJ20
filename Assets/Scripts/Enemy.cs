using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void Attack(string name)
    {
        GetComponentInChildren<Animator>().Play("dead_shark");

        if (name == "oxygen")
        {
            FindObjectOfType<OxygenRoom>().GetHit();
        }
        if (name == "bridge")
        {
            FindObjectOfType<BridgeRoom>().GetHit();
        }
        if (name == "powerplant")
        {
            FindObjectOfType<PowerPlantRoom>().GetHit();
        }
        if (name == "water")
        {
            FindObjectOfType<WaterStorageRoom>().GetHit();
        }
        if (name == "engine")
        {
            FindObjectOfType<EngineRoom>().GetHit();
        }
        if (name == "shield")
        {
            FindObjectOfType<ShieldRoom>().GetHit();
        }
        if (name == "tractor")
        {
            FindObjectOfType<Extractor_Beam>().GetHit();
        }
        if (name == "storage")
        {
            print(FindObjectOfType<Storage>());
            FindObjectOfType<Storage>().GetHit();
        }
    }
}
