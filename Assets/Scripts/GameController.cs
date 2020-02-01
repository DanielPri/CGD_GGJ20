using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // References to the rooms
    [SerializeField] private OxygenRoom _oxygenRoom;
    [SerializeField] private ShieldRoom _shieldRoom;
    [SerializeField] private BridgeRoom _bridgeRoom;
    [SerializeField] private EngineRoom _EngineRoom;
    [SerializeField] private PowerPlantRoom _powerPlantRoom;

    // Accounts for each part's damage state and some resource values like oxygen

    // Maybe this controller sets everything on game/scene start?

    // Update other rooms' important information as needed like Engine being functional in the Bridge room
    void Update()
    {
        if()
        {

        }
    }
}
