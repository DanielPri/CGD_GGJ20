using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageResourceUI : MonoBehaviour
{
    Text waterText;
    Text metalText;
    GameObject waterUI;
    GameObject metalUI;

    public RepairZone waterRoom;
    public RepairZone metalRoom;

    // Start is called before the first frame update
    void Start()
    {
        waterUI = transform.GetChild(0).gameObject;
        metalUI = transform.GetChild(1).gameObject;
        waterText = transform.GetChild(0).GetComponentInChildren<Text>();
        metalText = transform.GetChild(1).GetComponentInChildren<Text>();
        if (waterRoom == null) { waterRoom = GameObject.Find("WaterStorage").GetComponentInChildren<RepairZone>(); }
        
        if (metalRoom == null) { metalRoom = GameObject.Find("MetalStorage").GetComponentInChildren<RepairZone>(); }
        
    }

    // Update is called once per frame
    void Update()
    {
        waterText.text = Storage.resources_Water.ToString();
        metalText.text = Storage.resources_Metal.ToString();
        handleVisibility();
    }

    private void handleVisibility()
    {
        waterUI.SetActive(waterRoom.isOccupied);
        metalUI.SetActive(metalRoom.isOccupied);
    }
}
