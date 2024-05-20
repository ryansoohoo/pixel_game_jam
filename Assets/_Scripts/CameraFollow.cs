using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform boatPos;
    public Transform follow;
    public float slerpValue = 6f;
    Vector3 newPos;
    public bool inBoat;
    // Update is called once per frame
    void Update()
    {
        if (inBoat)
        {
            newPos = boatPos.position;
        }
        newPos = follow.position;
        newPos.z = -10;
        transform.position = newPos;
    }
}
