using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public OxygenRoom oxygenRoom;
    public float minAngle = 75f;
    public float maxAngle = -75f;

    private Transform needle;
    private float angles;
    private float currentAngle;
    // Start is called before the first frame update
    void Start()
    {
        needle = transform.GetChild(0);
        angles = maxAngle - minAngle;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = oxygenRoom.oxygenRemaining * angles + minAngle;
        needle.rotation = Quaternion.Euler(0,0, currentAngle);
    }
}
