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

    public int currentWave = 0;
    public void Awake()
    {
        if(instance == null)
            instance = this;
    }


    public Collectable GetRandomCollectable()
    {
        int rand = Random.Range(0, collectables.Count);
        Collectable collectable;
        for (int i = rand; i< collectables.Count; i++)
        {
            collectable = collectables[rand];
            if (collectable.image == null)
                i++;
            else
                return collectable;
        }
        return collectables[Random.Range(0, collectables.Count)];
    }
}

