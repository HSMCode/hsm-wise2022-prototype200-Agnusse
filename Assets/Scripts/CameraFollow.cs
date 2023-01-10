using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(target);
    }
}
