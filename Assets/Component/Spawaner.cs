﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawaner : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start ()
    {
        StartCoroutine (SpawnWaves ());
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {            
            int counter = 0;
            while(counter < hazardCount)
            {
                if(AppManager.IsUnpaused)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    counter++;
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);            
        }
    }
}
