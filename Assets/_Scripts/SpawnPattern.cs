using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnInfo
{
    public int spawnWave;
    public Collectable item;
    public bool hasBeenCollected;
}

public class SpawnPattern : MonoBehaviour
{
    public List<Collectable> collectables;
    public SpawnInfo[] dictionary;
    public static SpawnPattern instance;

    public void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
