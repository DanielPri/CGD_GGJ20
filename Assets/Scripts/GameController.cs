using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Important values
    [SerializeField] private int _TotalDamageToLose = 3;
    [SerializeField] private int _ScoreAdded = 10;
    [SerializeField] private int _ScoreModifier = 1;

    // References to the rooms
    [SerializeField] private OxygenRoom _OxygenRoom;
    [SerializeField] private ShieldRoom _ShieldRoom;
    [SerializeField] private BridgeRoom _BridgeRoom;
    [SerializeField] private EngineRoom _EngineRoom;
    [SerializeField] private PowerPlantRoom _PowerPlantRoom;

    // Control variables
    private int _DamageCount;
    private int _Score; // TODO: Check how scoreboard works later
    private float _OxygenRemaining; // Get it from OxygenRoom

    // Maybe this controller sets everything on game/scene start?

    // Sets initial values
    void Start()
    {
        _DamageCount = 0;
        _Score = 0;
        _OxygenRemaining = 1f;
    }

    // Update other rooms' important information as needed like Engine being functional in the Bridge room
    void Update()
    {
        // Check for lose condition
        CheckDamage();

        // Check for points to score
        CheckPiloting();
    }

    // Check if engine is operational or not (is only working again once its fully repaired)
    private void CheckEngine()
    {
        if (_EngineRoom.damageState == DAMAGE_STATE.FUNCTIONAL)
        {
            _BridgeRoom.isEngineFunctional = true;
        }
        else if(_EngineRoom.damageState == DAMAGE_STATE.DESTROYED)
        {
            _BridgeRoom.isEngineFunctional = false;
            _DamageCount++;
        }
    }

    // Checks and updates overall damage of each room
    private void CheckDamage()
    {
        if(_OxygenRoom.damageState == DAMAGE_STATE.DESTROYED)
        {
            _DamageCount++;
        }

        if (_ShieldRoom.damageState == DAMAGE_STATE.DESTROYED)
        {
            _DamageCount++;
        }

        if (_BridgeRoom.damageState == DAMAGE_STATE.DESTROYED)
        {
            _DamageCount++;
        }

        // Checks engine damage and updates bridge if necessary
        CheckEngine();

        if(_DamageCount >= _TotalDamageToLose)
        {
            // Then lose I guess
            Debug.Log("YOU LOSE! Or rather, you sink (?)");
        }
    }

    // Checks if the ship is being piloted and adds points to the score
    private void CheckPiloting()
    {
        if(_BridgeRoom.isPiloting)
        {
            // update score accordingly
            _Score += _ScoreAdded * _ScoreModifier;
            // update score UI
            // TODO: sinchronize with scoreboard (either here or in the scoreboard script)
        }
    }

    // maybe do a score getter

    // 
}
