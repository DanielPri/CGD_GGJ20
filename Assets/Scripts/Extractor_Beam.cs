using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor_Beam : MonoBehaviour
{
    public static bool extracting;
    public static Vector3 extractor_beam_position;

    // Start is called before the first frame update
    void Start()
    {
        extractor_beam_position = transform.position;
        extracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Extract"))
        {
            extracting = true;
        }
        if (Input.GetButtonUp("Extract"))
        {
            extracting = false;
        }
    }
}
