using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    Color red = new Color(1.0f, 0.0f, 0.0f);
    Color green = new Color(0.0f, 1.0f, 0.0f);
    Color blue = new Color(0.0f, 0.0f, 1.0f);
    public Color[] colors = new Color[3];
    public Color currentColor;
    public float timer;
    public string keyToBePressed;
    public string keyPressed;

    public GameObject barTimer;
    public Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 endScale = new Vector3(1.0f, 0.0f, 1.0f);
    private float elapsedTime = 0.0f;
    public float speed;

    public Text text;
    public int repair;


    // Start is called before the first frame update
    private void Start()
    {
        colors[0] = red;
        colors[1] = green;
        colors[2] = blue;

        int index = Random.Range(0, colors.Length);
        currentColor = colors[index];
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
        //StartCoroutine(wait());
    }

    private void Update()
    {
        barTimer.transform.localScale = Vector3.Lerp(startScale, endScale, speed*elapsedTime);
        elapsedTime += Time.deltaTime;

        if (barTimer.transform.localScale.y == 0.0f || Input.GetKeyDown("q") || Input.GetKeyDown("l"))
        {
            DestroyUnit();
        }

        keyPressed = Input.inputString;
        setKey();
        setKeyPressed();
        playerKey();
        text.text = "Health: " + repair;
        checkBar();
    }

    /*
    IEnumerator wait()
    {
        while (true)
        {
            int index = Random.Range(0, colors.Length);
            currentColor = colors[index];
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
            yield return new WaitForSeconds(timer);
        }
    }
    */

    private void setKey()
    {
        if (currentColor == red) {
            keyToBePressed = "a";
        }
        if (currentColor == green) {
            keyToBePressed = "w";
        }
        if (currentColor == blue) {
            keyToBePressed = "d";
        }
    }

    private void setKeyPressed()
    {
        if (Input.GetKeyDown("d"))
        {
            keyPressed = "d";
            DecreaseTimer();
        }
        if (Input.GetKeyDown("w"))
        {
            keyPressed = "w";
            DecreaseTimer();
        }
        if (Input.GetKeyDown("a"))
        {
            keyPressed = "a";
            DecreaseTimer();
        }
    }

    private void playerKey()
    {
        if (keyPressed == keyToBePressed) {
            int index = Random.Range(0, colors.Length);
            currentColor = colors[index];
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
            repair++;
        }
    }

    private void DestroyUnit()
    {
        Destroy(barTimer);
        Destroy(this.gameObject);
    }

    private void DecreaseTimer()
    {
        if (keyPressed != keyToBePressed)
        {
            if (barTimer.transform.localScale.y > 0)
            {
                startScale -= new Vector3(0.0f, 0.3f, 0.0f);
                speed += 0.1f;
            }
            else
            {
                DestroyUnit();
            }
        }
    }

    private void checkBar()
    {
        if (barTimer.transform.localScale.y <= 0)
        {
            DestroyUnit();
        }
    }
    

}
