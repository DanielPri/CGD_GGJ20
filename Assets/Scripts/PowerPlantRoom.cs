using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantRoom : RoomComponent
{
    [HideInInspector] public float totalEnergy { get; private set; } = 100f;
    [HideInInspector] public float currentEnergy; //TODO replace with Storage.energy
    [SerializeField] float regenEnergy; //Amount regenerated overtime
    [SerializeField] float regenCooldown; //Time between each regeneration
    [SerializeField] float burnedRessources; //Amount of energy gained when burning ressources (ability)

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("regenerateEnergy", regenCooldown, regenCooldown);
    }

    void regenerateEnergy() { //Overtime regeneration
        if (currentEnergy < totalEnergy) {
            currentEnergy += regenEnergy;
            if (currentEnergy > totalEnergy) currentEnergy = totalEnergy;
        }
    }

    public void burnRessources() {
        currentEnergy += burnedRessources;
    }

    protected override void doAction() {
        if(/*Storage.metal < burnedRessources &&*/ currentEnergy < totalEnergy) {
            //Storage.metal -= burnedRessources;
            currentEnergy += burnedRessources;
            if (currentEnergy > totalEnergy) currentEnergy = totalEnergy;
        }
    }

    protected override void endAction() {
    }
}
