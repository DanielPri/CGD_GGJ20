using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    public Sprite[] healthBar = new Sprite[4];
    public int counter;
    private int index;

    private void Start()
    {
        healthBar[0] = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        healthBar[1] = transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        healthBar[2] = transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        healthBar[3] = transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        counter = controller.getDamagedRooms();
        index = healthBar.Length - counter;
        DestroyHealth();
    }

    void DestroyHealth()
    {
        if (index < 4 && index >=0)
        {
            healthBar[index] = red;
            transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = red;
        }
    }
}
