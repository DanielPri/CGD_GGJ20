using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject[] rooms = new GameObject[5];
    public int maxHealth = 100;
    public int currentHealth;


    private Transform bar;

    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find("Bar");
    }

    // Update is called once per frame
    private void Update()
    {   
        currentHealth = 0;
        for (int i = 0; i < rooms.Length; i++) {
            currentHealth += rooms[i].GetComponent<ChangeColor>().health;
        }
        bar.transform.localScale = new Vector2(currentHealth/100/0f, 1);
    }   
}
