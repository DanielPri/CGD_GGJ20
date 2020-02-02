using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Important values
    [SerializeField] private int _TotalDamageToLose = 4;
    [SerializeField] private int _ScoreAdded = 10;
    [SerializeField] private int _ScoreModifier = 1;

    // References
    [SerializeField] private OxygenRoom _OxygenRoom;
    [SerializeField] private ShieldRoom _ShieldRoom;
    [SerializeField] private BridgeRoom _BridgeRoom;
    [SerializeField] private EngineRoom _EngineRoom;
    [SerializeField] private Storage _StorageRoom;
    [SerializeField] private WaterStorageRoom _WaterRoom;
    [SerializeField] private Extractor_Beam _TractorRoom;
    [SerializeField] private PowerPlantRoom _PowerPlantRoom;
    [SerializeField] private Persistent _persistent;

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource intense_music;
    bool wasIntenseMusic;

    // Control variables
    private int _Score; // TODO: Check how scoreboard works later
    private float _OxygenRemaining; // Get it from OxygenRoom

    // Maybe this controller sets everything on game/scene start?

    // Sets initial values
    void Start()
    {
        wasIntenseMusic = false;
        _Score = 0;
        _OxygenRemaining = 1f;
        music.Play();
    }

    // Update other rooms' important information as needed like Engine being functional in the Bridge room
    void Update()
    {
        // Check for lose condition
        CheckDamage();

        // Check for points to score
        CheckPiloting();
        
        // Update oxygen remaining from the room
        _OxygenRemaining = _OxygenRoom.oxygenRemaining;
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
        }
    }

    // Checks and counts total damage
    private void CheckDamage()
    {
        // Check if each room is damaged
        int countDamage = 0;

        if (_OxygenRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_ShieldRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_BridgeRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_EngineRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_PowerPlantRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_StorageRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_TractorRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;
        if (_WaterRoom.damageState == DAMAGE_STATE.DESTROYED)
            countDamage++;

        // Checks engine damage and updates bridge if necessary
        CheckEngine();

        if (countDamage >= 3)
        {
            music.Stop();
            intense_music.Play();
            wasIntenseMusic = true;
        }
        if (countDamage < 3 && wasIntenseMusic)
        {
            music.Play();
            intense_music.Stop();
            wasIntenseMusic = false;
        }

        if (countDamage >= _TotalDamageToLose)
        {
            // Then lose I guess
            Debug.Log("LOSE!");
            // Save score
            _persistent.SetPersistScore(_Score);
            // Change scene
            SceneManager.LoadScene("GameOver");
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
        }
    }
}
