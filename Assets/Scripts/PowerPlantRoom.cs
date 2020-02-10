using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantRoom : RoomComponent
{
    [HideInInspector] public float totalEnergy { get; private set; } = 100f;
    [HideInInspector] public static float currentEnergy = 1; //TODO replace with Storage.energy
    [SerializeField] float regenEnergy; //Amount regenerated overtime
    [SerializeField] float regenCooldown; //Time between each regeneration
    [SerializeField] float burnedRessources; //Amount of energy gained when burning ressources (ability)
    [SerializeField] float metalSlowdown = 1f;
    [SerializeField] float waterSlowdown = 1f;
    [SerializeField] float powerMultiplier = 10f;

    // Start is called before the first frame update
    new void Start()
    {
        repairZone = GetComponentInChildren<RepairZone>();
        if (transform.Find("DestrucitonFlames") != null)
        {
            destructionFlames = transform.Find("DestrucitonFlames").gameObject;
        }
        if (transform.Find("Smoke") != null)
        {
            destructionSmoke = transform.Find("Smoke").gameObject;
        }
        spriteRenderer = transform.Find("PowerCore").Find("DullCore").GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
        currentEnergy = 50; //this isn't working for some reason?
        InvokeRepeating("regenerateEnergy", regenCooldown, regenCooldown);
    }

    void regenerateEnergy() { //Overtime regeneration
        if (currentEnergy < totalEnergy) {
            currentEnergy += regenEnergy * Time.deltaTime * 0.2f;
            if (currentEnergy > totalEnergy) currentEnergy = totalEnergy;
        }
    }

    new void Update()
    {
        regenerateEnergy();
        base.Update();
    }

    public void burnRessources() {
        currentEnergy += burnedRessources;
    }
    
    protected override void doAction() {
        if(/*Storage.metal < burnedRessources &&*/ currentEnergy < totalEnergy) {
            currentEnergy += (metalCost + waterCost) * Time.deltaTime * powerMultiplier;
            Storage.resources_Metal -= metalCost * Time.deltaTime * metalSlowdown;
            WaterStorageRoom.resources_Water -= waterCost * Time.deltaTime * waterSlowdown;
            if (currentEnergy > totalEnergy) currentEnergy = totalEnergy;
        }
    }

    protected override void endAction() {
    }
}
