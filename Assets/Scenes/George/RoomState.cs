using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomState : MonoBehaviour
{
    public int state;

    void Start() {
        state = 3;
    }

    void Update() {
       // t += Time.deltaTime/duration;
       // this.GetComponent<SpriteRenderer>().material.color = Color.Lerp(startColor, endColor, t);
        if (state < 1) {
            state = 1;
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            state -= 1;
        }
    }
}
