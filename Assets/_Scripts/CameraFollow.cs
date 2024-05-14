using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public float slerpValue = 6f;
    Vector3 newPos;
    // Update is called once per frame
    void Update()
    {
        newPos = follow.position;
        newPos.z = -10;
        transform.position = newPos;
    }
}
