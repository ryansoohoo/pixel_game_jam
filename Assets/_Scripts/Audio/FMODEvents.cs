using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Hat Collect SFX")]
    [field: SerializeField] public EventReference hatCollected { get; private set; }

    [field: Header("Otter Swim SFX")]
    [field: SerializeField] public EventReference otterSwim { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            UnityEngine.Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
