using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 endScale = new Vector3(1.0f, 0.0f, 1.0f);

    private float elapsedTime = 0.0f;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime);
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1)
        {
            elapsedTime = 0;
        }
    }
}
