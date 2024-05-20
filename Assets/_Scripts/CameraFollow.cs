using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform boatPos;
    public Transform follow;
    public float slerpValue = 6f;
    Vector3 newPos;
    public bool inBoat;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (inBoat)
        {
            newPos = boatPos.position;
        }else
            newPos = follow.position;
        newPos.z = -10;
        transform.position = Vector3.Slerp(transform.position,newPos,7*Time.deltaTime);
    }
}
