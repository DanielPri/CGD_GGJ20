using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [SerializeField] private AnimationClip Oxygen;
    [SerializeField] private AnimationClip Engine;
    [SerializeField] private AnimationClip Water;
    [SerializeField] private AnimationClip PowerPlant;
    [SerializeField] private AnimationClip Storage;
    [SerializeField] private AnimationClip Bridge;
    [SerializeField] private AnimationClip Tractor;
    [SerializeField] private AnimationClip Shield;

    [SerializeField] private GameObject enemyPrefab;
    GameObject newEnemy;

    [SerializeField] float spawnDelay;
    float nextTimeToSpawn = 0f;

    void Update()
    {
        if (nextTimeToSpawn <= Time.time) //Time.time is the number of seconds elapsed since the start of the game
        {
            SpawnEnemy();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnEnemy()
    {
        int option = Random.Range(1, 9);

        switch (option)
        {
            case 1:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50,50,50), Quaternion.identity);
                newEnemy.GetComponent<Animator>().Play("Attack_Oxygen_Room");
                break;
            case 2:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.transform.localScale = new Vector3(-1, 1, 1);
                newEnemy.GetComponent<Animator>().Play("Attack_Engine_Room");
                break;
            case 3:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.GetComponent<Animator>().Play("Attack_Storage_Room");
                break;
            case 4:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.transform.localScale = new Vector3(-1, 1, 1);
                newEnemy.GetComponent<Animator>().Play("Attack_PowerPlant_Room");
                break;
            case 5:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.GetComponent<Animator>().Play("Attack_Water_Room");
                break;
            case 6:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.GetComponent<Animator>().Play("Attack_Tractor_Room");
                break;
            case 7:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.GetComponent<Animator>().Play("Attack_Shield_Room");
                break;
            case 8:
                newEnemy = Instantiate(enemyPrefab, new Vector3(50, 50, 50), Quaternion.identity);
                newEnemy.transform.localScale = new Vector3(-1, 1, 1);
                newEnemy.GetComponent<Animator>().Play("Attack_Bridge_Room");
                break;
            default:
                break;
        }

    }
}
