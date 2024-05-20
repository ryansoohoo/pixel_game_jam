using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableState
{
    Level,PlayerHeld,OnFishingLine,OnBoat
}

public enum CharacterLikings
{
    Omar,Olga,Oscar,Octavia
}
[CreateAssetMenu(fileName = "NewCollectableData", menuName = "ScriptableObjects/NewCollectableData", order = 1)]
public class Collectable : ScriptableObject
{
    public Sprite image;
    public Sprite imageDown;
    public Sprite imageNotCollected;
    public AnimationClip idle;
    public AnimationClip down;

    public CharacterLikings characterLiking;
    public string itemName;
    public string desc;

    public float dropRate; // 0-1
    public int weight;
    public int itemIndex;
}
