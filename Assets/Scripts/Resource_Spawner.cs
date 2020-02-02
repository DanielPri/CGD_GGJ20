using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Spawner : MonoBehaviour
{

    [SerializeField] private int radius;
    [SerializeField] private GameObject Metal;
    [SerializeField] private GameObject Energy;
    [SerializeField] private GameObject Water;

    //their spawning coordinates
    float coordX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    //3 probabilities. Should add up to 100
    [SerializeField] int probability1;
    [SerializeField] int probability2;
    [SerializeField] int probability3;

    //desired time between each spawn.
    [SerializeField] float spawnDelay;
    float nextTimeToSpawn = 0f;

    private void Start()
    {
        coordX = GetComponent<Transform>().position.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (nextTimeToSpawn <= Time.time) //Time.time is the number of seconds elapsed since the start of the game
        {
            SpawnResource();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    //spawning gold depends on probability
    void SpawnResource()
    {
        float coordY = Random.Range(minY, maxY);
        float coordZ = Random.Range(minZ, maxZ);
        float prob = Random.Range(0, 100);

        //assuming the probability of gold > gold2 > gold3
        if (prob <= probability1)
        {
            Instantiate(Metal, new Vector3(coordX, coordY, coordZ), Metal.transform.rotation);
        }
        else if ((prob <= probability2 + probability1) && (prob > probability1))
        {
            Instantiate(Energy, new Vector3(coordX, coordY - 1f, coordZ), Energy.transform.rotation);
        }
        else if ((prob <= probability3 + probability2 + probability1) && (prob > probability2 + probability1))
        {
            Instantiate(Water, new Vector3(coordX, coordY, coordZ), Water.transform.rotation);
        }
    }
}
