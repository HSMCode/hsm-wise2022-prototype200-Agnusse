using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public Transform target;
    public Vector3 offset;

    //Camera follows Player
    void LateUpdate()
    {
        //changes position of camera
        transform.position = player.transform.position + offset;

        // makes camera look at the target
        transform.LookAt(target);
    }
}
