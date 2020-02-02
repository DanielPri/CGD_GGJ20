using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // Color Settings (will change to arrows)
    Color red = new Color(1.0f, 0.0f, 0.0f);
    Color green = new Color(0.0f, 1.0f, 0.0f);
    Color blue = new Color(0.0f, 0.0f, 1.0f);
    public Color[] colors = new Color[3];
    public Color currentColor;
    public float timer;

    // Key Settings
    public string keyToBePressed;
    public string keyPressed;

    // Timer Settings
    public GameObject barTimer;
    public Vector3 startScale;
    public Vector3 endScale;
    private float elapsedTime = 0.0f;
    public float speed = 0.1f;

    // Text Settings (Debugging)
    public int repair;

    // Room Settings
    public RoomComponent room;


    // Start is called before the first frame update
    private void Start() {
        colors[0] = red;
        colors[1] = green;
        colors[2] = blue;

        startScale = transform.localScale;
        endScale = new Vector3(transform.localScale.x, 0.0f, transform.position.z);

        int index = Random.Range(0, colors.Length);
        currentColor = colors[index];
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
    }

    private void Update() {
        barTimer.transform.localScale = Vector3.Lerp(startScale, endScale, speed*elapsedTime);
        elapsedTime += Time.deltaTime;

        if (barTimer.transform.localScale.y == 0.0f || Input.GetKeyDown("q") || Input.GetKeyDown("l")) {
            DestroyUnit();
        }

        //keyPressed = Input.inputString;
        setKey();
        setKeyPressed();
        playerKey();
        convertState();
    }

    private void setKey()
    {
        if (currentColor == red)
            keyToBePressed = "a";

        if (currentColor == green)
            keyToBePressed = "w";

        if (currentColor == blue)
            keyToBePressed = "d";
    }

    private void setKeyPressed()
    {
        if (Input.GetKeyDown("d")) {
            keyPressed = "d";
            DecreaseTimer();
        }
        if (Input.GetKeyDown("w")) {
            keyPressed = "w";
            DecreaseTimer();
        }
        if (Input.GetKeyDown("a")) {
            keyPressed = "a";
            DecreaseTimer();
        }
    }

    private void playerKey() {
        if (keyPressed == keyToBePressed) {
            int index = Random.Range(0, colors.Length);
            currentColor = colors[index];
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
            repair++;
            keyPressed = null;
        }
    }

    private void DestroyUnit() {
        Destroy(barTimer);
        Destroy(this.gameObject);
    }

    private void DecreaseTimer()
    {
        if (keyPressed != keyToBePressed)
        {
            if (barTimer.transform.localScale.y > 0) {
                startScale -= new Vector3(0.0f, 0.3f, 0.0f);
                speed += 0.1f;
            }
            else
                DestroyUnit();
        }
    }

    private void convertState() {

        OxygenRoom state = room.GetComponent<OxygenRoom>();

        if (repair <= 0)
        {
            state.damageState = DAMAGE_STATE.DESTROYED;
        }
        else if (repair > 0 && repair < 7)
        {
            state.damageState = DAMAGE_STATE.DAMAGED;
        }
        else if (repair > 7 && repair <= 15)
        {
            state.damageState = DAMAGE_STATE.FUNCTIONAL;
        }
    }
    

}
