using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject[] rooms = new GameObject[5];
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    private void Start()
    {
        //bar = transform.Find("Bar");
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {   
        /*
        for (int i = 0; i < rooms.Length; i++) {
            currentHealth += rooms[i].GetComponent<ChangeColor>().health;
        }
        bar.transform.localScale = new Vector2(currentHealth/100/0f, 1);
        */
        calculateHealth();
    }

    private void calculateHealth() {
        int temp = 0;
        foreach (GameObject room in rooms) {
            temp += room.GetComponent<RoomState>().state;
        }
        currentHealth = temp;
        this.transform.localScale = new Vector2(currentHealth/15.0f, 1.0f);
    }
}
