using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableState
{
    Level,PlayerHeld,OnFishingLine,OnBoat
}
[CreateAssetMenu(fileName = "NewCollectableData", menuName = "ScriptableObjects/NewCollectableData", order = 1)]
public class Collectable : ScriptableObject
{
    public Sprite image;

    public string itemName;
    public string desc;

    public float dropRate; // 0-1
    public int weight;
    public int itemIndex;
}
