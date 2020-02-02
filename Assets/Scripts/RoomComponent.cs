using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    public DAMAGE_STATE damageState = DAMAGE_STATE.FUNCTIONAL;
    public float repairSpeed = 2f;

    protected RepairZone repairZone;
    protected bool canRepair = false;
    [Range(0.0f, 1.0f)]
    public float repairProgress = 0f;
    protected PLAYER occupyingPlayer = PLAYER.NONE;

    [HideInInspector] public bool activated { get; protected set; } = false; //Whether the ability is activated or not
    [HideInInspector] public bool onCooldown { get; protected set; } = false; //Whether the ability is on cooldown or not
    [SerializeField] protected float abilityCooldown; //Cooldown time
    [SerializeField] protected float abilityDuration; //How long the ability stays active
    protected float abilityTimer = 0; //Calculates the duration and the cooldown
    public float energyCost { get; protected set; } //Energy cost of the ability

    void Start()
    {
        repairZone = GetComponentInChildren<RepairZone>();
    }

    protected void Update()
    {
        setRepairStatus();
        checkOccupyingPlayer();
        handleInput();
        // handle room stuff like light turning on;
    }

    private void checkOccupyingPlayer()
    {
        occupyingPlayer = repairZone.occupyingPlayer;
    }

    void handleInput()
    {
        //ALSO the button names are placeholders until we decide on the button
        if (Input.GetButton("Player1Repair") && occupyingPlayer == PLAYER.PLAYER1)
        {
            repair();
            Debug.Log("Repairing!");
        }
        if (Input.GetButton("Player2Repair") && occupyingPlayer == PLAYER.PLAYER2)
        {
            repair();
        }
        if (Input.GetButton("Player1Action") && occupyingPlayer == PLAYER.PLAYER1)
        {
            doAction();
        }
        if (Input.GetButton("Player2Action") && occupyingPlayer == PLAYER.PLAYER2)
        {
            doAction();
        }
        if (Input.GetButtonUp("Player1Action") && occupyingPlayer == PLAYER.PLAYER1)
        {
            endAction();
        }
        if (Input.GetButtonUp("Player2Action") && occupyingPlayer == PLAYER.PLAYER2)
        {
            endAction();
        }

    }
    
    /// <summary>
    /// What happens to the action when the player releases the button
    /// </summary>
    virtual protected void endAction()
    {
        // implement pls
    }

    /// <summary>
    ///  The action to be performed while a player is holding the action button in the zone
    /// </summary>
    virtual protected void doAction()
    {
        // implement this in the extending class
    }

    protected void repair()
    {
        if (canRepair)
        {
            repairProgress += repairSpeed * Time.deltaTime;
            if (repairProgress >= 1f)
            {
                changeDamageState();
                repairProgress = 0f;
            }
        }
    }

    void changeDamageState()
    {
        if (damageState == DAMAGE_STATE.DESTROYED)
        {
            damageState = DAMAGE_STATE.DAMAGED;
        }
        else if (damageState == DAMAGE_STATE.DAMAGED)
        {
            damageState = DAMAGE_STATE.FUNCTIONAL;
        }
        else
        {
            Debug.Log("HEY WHAT ARE YOU DOING HERE THIS SHOULDNT BE POSSIBLE");
        }
    }

    void setRepairStatus()
    {
        if (damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            canRepair = false;
        }
        else
        {
            canRepair = repairZone.isOccupied;
        }

    }
}
