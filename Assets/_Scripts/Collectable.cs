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
    public string itemName;
    public Sprite image;
    public float dropRate; // 0-1
    public int weight; 
}
