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
        collectables.Shuffle();
    }


    public Collectable GetRandomCollectable()
    {
        Collectable collectable = collectables[Random.Range(0, collectables.Count)];
        collectables.Remove(collectable);
        return collectable;
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}