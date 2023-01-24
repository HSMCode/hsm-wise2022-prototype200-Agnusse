using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveFan : MonoBehaviour
{
    private Rigidbody _enemyRb;
    private GameObject _player;
    // private UpdateScoreTimer _updateScoreTimerScript;

    [SerializeField] float speed;


    private Updater _Updater;

    
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        
        // make sure to set the tag "Player" on your player character for this to work
        _player = GameObject.FindWithTag("Player");

        _Updater = GameObject.Find("Updater").GetComponent<Updater>();
    }
    
    void FixedUpdate()
    {
        // move the enemy to the vector position of the player
        _enemyRb.AddForce((_player.transform.position - transform.position).normalized * speed);
        // Debug.Log("Player: " + _player.transform.position + "Enemy: " + transform.position);
    }


    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // _Updater.reduceLives(); 

            // _updateScoreTimerScript.AddEnemiesCounter();
            Destroy(this.gameObject);
        }
    }


    // For debugging we can add gizmos to help visualise depth and distance a bit better
    void OnDrawGizmosSelected()
    {
        if (_player != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _player.transform.position);
        }
    }
}