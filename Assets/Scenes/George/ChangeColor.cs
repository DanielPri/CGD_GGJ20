﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHealth : MonoBehaviour
{
    public int health;

    void Start() {
        health = 2;
    }

    void Update() {
       // t += Time.deltaTime/duration;
       // this.GetComponent<SpriteRenderer>().material.color = Color.Lerp(startColor, endColor, t);
        if (health < 0) {
            health = 0;
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            health -= 1;
        }
    }
}