using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    SpriteRenderer[] healthBar = new SpriteRenderer[4];
    public int counter;
    private int index;

    private void Start()
    {
        healthBar[0] = transform.GetChild(0).GetComponent<SpriteRenderer>();
        healthBar[1] = transform.GetChild(1).GetComponent<SpriteRenderer>();
        healthBar[2] = transform.GetChild(2).GetComponent<SpriteRenderer>();
        healthBar[3] = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        counter = controller.getDamagedRooms();
        //index = healthBar.Length - counter;
        //DestroyHealth();

        switch (counter)
        {
            case 0:
                healthBar[0].sprite = green;
                healthBar[1].sprite = green;
                healthBar[2].sprite = green;
                healthBar[3].sprite = green;
                break;
            case 1:
                healthBar[0].sprite = green;
                healthBar[1].sprite = green;
                healthBar[2].sprite = green;
                healthBar[3].sprite = red;
                break;
            case 2:
                healthBar[0].sprite = green;
                healthBar[1].sprite = green;
                healthBar[2].sprite = red;
                healthBar[3].sprite = red;
                break;
            case 3:
                healthBar[0].sprite = green;
                healthBar[1].sprite = red;
                healthBar[2].sprite = red;
                healthBar[3].sprite = red;
                break;

        }
    }
}
