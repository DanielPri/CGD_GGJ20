using UnityEngine;
using System.Collections;

public class Storage : MonoBehaviour
{

    public static int resources_Water;
    public static int resources_Metal;
    public static int resources_Energy;

    void Start()
    {
        resources_Water = 0;
        resources_Metal = 0;
        resources_Energy = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
