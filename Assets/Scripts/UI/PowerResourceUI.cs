using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerResourceUI : MonoBehaviour
{
    Text powerText;
    GameObject powerUI;

    public RepairZone powerRoom;

    void Start()
    {
        powerUI = transform.GetChild(0).gameObject;
        powerText = transform.GetChild(0).GetComponentInChildren<Text>();
        if (powerRoom == null) { powerRoom = GameObject.Find("PowerPlantRoom").GetComponentInChildren<RepairZone>(); }
        
    }

    // Update is called once per frame
    void Update()
    {
        powerText.text = PowerPlantRoom.currentEnergy.ToString();
        handleVisibility();
    }
    private void handleVisibility()
    {
        powerUI.SetActive(powerRoom.isOccupied);
    }
}
