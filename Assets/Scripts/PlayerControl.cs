using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public float Acceleration;

    public float CurrentSpeed;
    public float TargetSpeed;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        TargetSpeed = Input.GetAxisRaw("Horizontal") * Speed;
         
        
    }
}
