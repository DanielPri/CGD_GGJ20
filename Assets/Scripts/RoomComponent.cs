using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    public DAMAGE_STATE damageState = DAMAGE_STATE.FUNCTIONAL;
    public float repairSpeed = 4f;

    protected RepairZone repairZone;
    protected bool canRepair = false;
    [Range(0.0f, 1.0f)]
    public float repairProgress = 0f;
    protected PLAYER occupyingPlayer = PLAYER.NONE;
    public GameObject destructionFlames;


    [HideInInspector] public bool activated { get; protected set; } = false; //Whether the ability is activated or not
    [HideInInspector] public bool onCooldown { get; protected set; } = false; //Whether the ability is on cooldown or not
    [SerializeField] protected float abilityCooldown; //Cooldown time
    [SerializeField] protected float abilityDuration; //How long the ability stays active
    protected float abilityTimer = 0; //Calculates the duration and the cooldown
    public float energyCost { get; protected set; } //Energy cost of the ability
    protected SpriteRenderer spriteRenderer;

    [Range(1, 50)]
    [SerializeField] private int arrowRotationSpeed = 30;
    [SerializeField] private GameObject arrowRotator, circle, space, pointsUi;

    private AudioSource audioSource;
    private float arrowRotation = 0.0f;
    private bool isSkillCheckOn = false;
    protected void Start()
    {
        repairZone = GetComponentInChildren<RepairZone>();
        if (transform.Find("DestrucitonFlames") != null)
        {
            destructionFlames = transform.Find("DestrucitonFlames").gameObject;
        }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Update()
    {
        setRepairStatus();
        checkOccupyingPlayer();
        handleInput();
        if (isSkillCheckOn)
        {
            DoSkillCheck();
        }
        // handle room stuff like light turning on;
        handleFlames();
        changeColor();
    }

    private void changeColor()
    {
        if(spriteRenderer != null)
        {
            if(damageState == DAMAGE_STATE.FUNCTIONAL)
            {
                spriteRenderer.color = new Color(1,1,1); //default
            }
            if (damageState == DAMAGE_STATE.DAMAGED)
            {
                spriteRenderer.color = new Color(1, 0.5882353f, 0.5882353f); //Pinkish
            }
            if (damageState == DAMAGE_STATE.DESTROYED)
            {
                spriteRenderer.color = new Color(1, 0.1843137f, 0); //Redish
            }
        }
    }

    private void handleFlames()
    {
        if (destructionFlames != null)
            if (damageState == DAMAGE_STATE.DESTROYED)
            {
                destructionFlames.SetActive(true);
            }
            else
            {
                destructionFlames.SetActive(false);
            }
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
            SkillCheckStarter();
            
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

    void SkillCheckStarter()
    {
        Vector3 currentCircleRotation = circle.transform.rotation.eulerAngles;
        circle.transform.rotation = Quaternion.Euler(currentCircleRotation.x, currentCircleRotation.y, UnityEngine.Random.Range(160, 360));
        SetActiveGameUI(true);
        isSkillCheckOn = true;
    }
    void SetActiveGameUI(bool isActive)
    {
        arrowRotator.SetActive(isActive);
        circle.SetActive(isActive);
        space.SetActive(isActive);
    }
    void DoSkillCheck()
    {
        // Rotate the arrow and store current rotation angle
        arrowRotation += Time.deltaTime * arrowRotationSpeed * -10;
        arrowRotator.transform.Rotate(0, 0, Time.deltaTime * arrowRotationSpeed * -10, Space.World);

        if (Input.GetKeyDown("space"))
        {
            // Arrow in good skill check zone
            if (arrowRotator.transform.rotation.eulerAngles.z >= circle.transform.rotation.eulerAngles.z - 158
                && arrowRotator.transform.rotation.eulerAngles.z < circle.transform.rotation.eulerAngles.z - 120)
            {
                pointsUi.SetActive(true);
                Invoke("DeactivatePointsUI", 3);
                FinishSkillCheck(true);
            }

            // Arrow not in skill check zone
            else
            {
                FinishSkillCheck(false);
            }
        }
        // Arrow went full circle
        if (-arrowRotation >= 360)
        {
            FinishSkillCheck(false);
        }

    }
    void FinishSkillCheck(bool result)
    {
        isSkillCheckOn = false;
        arrowRotation = 0.0f;
        arrowRotator.transform.rotation = Quaternion.Euler(Vector3.zero);
        SetActiveGameUI(false);
        if (result)
        {
            repairProgress += repairSpeed * Time.deltaTime*10;
            if (repairProgress >= 1f)
            {
                changeDamageState();
                repairProgress = 0f;
            }
        }
        else
        {
            repairProgress += repairSpeed * Time.deltaTime;
            if (repairProgress >= 1f)
            {
                changeDamageState();
                repairProgress = 0f;
            }
        }
        pointsUi.SetActive(false);
    }

    public void GetHit()
    {
        damageState++;
    }
}
