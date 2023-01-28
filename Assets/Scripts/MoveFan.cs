using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveFan : MonoBehaviour
{
    // Player components
    private Rigidbody _enemyRb;
    private Animator _animator;
    private GameObject _player;

    // give Fan runspeed
    [SerializeField] float speed;

    // link to PlayerController-Script
    private PlayerController _PlayerController;

    // link to Updater-Script
    private Updater _Updater;
    
     // Start is called before the first frame update
    void Start()
    {
        // get player Components
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody>();
        
        // Find Object Player
        _player = GameObject.FindWithTag("Player");

        // find PlayerController-Script
        _PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // find Updater-Script
        _Updater = GameObject.Find("Updater").GetComponent<Updater>();
    }
    
    void FixedUpdate()
    {
        // runs script if the game is running in PlayerController_Script 
        if (_PlayerController.isGameOn())
        {
            // move the enemy to the vector position of the player
            _enemyRb.AddForce((_player.transform.position - transform.position).normalized * speed);
        }
    }

    // when gameobject Fan collides
    private void OnCollisionEnter(Collision other)
    {
        // compares if Fan collides with Player and if that is the case runs script
        if(other.gameObject.CompareTag("Player"))
        {
            // Fan gets destroyed
            Destroy(this.gameObject);
        }
    }
}