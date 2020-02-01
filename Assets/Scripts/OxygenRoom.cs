using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRoom : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float oxygenRemaining = 1f;
    public DAMAGE_STATE damageState = DAMAGE_STATE.FUNCTIONAL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
