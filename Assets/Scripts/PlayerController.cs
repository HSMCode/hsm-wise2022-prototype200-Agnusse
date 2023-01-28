using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables to move Elvis
    private float _horizontalInput;
    private float _forwardInput;
    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    // variables to make Elvis dance
    private bool _isDancing;
    private bool _isNotDancing;

    // player components
    private Animator _animator;
    private Rigidbody _playerRb;

    // variables for counting hits
    public int elvisHit = 1;
    public int fanHit = 1;

    // win condition
    public bool elvisHome;

    // to control if the game is still running or over
    private bool _gameOn;

    //link to Updater-Script
    private Updater _updater;
    
    // Start is called before the first frame update
    void Start()
    {
        //Starts the game
        _gameOn = true;

        //sets to basic value not dancing
        _isDancing = false;

        //sets the goal to not reached
        elvisHome = false;

        // get player components
        _playerRb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        //find Updater-Script
        _updater = GameObject.Find("Updater").GetComponent<Updater>();
    }

    // Update is called once per frame
    void Update()
    {
        //runs the main script while GameOn
        if (_gameOn)
        {
            //Elvis moves
            _horizontalInput = Input.GetAxis("Horizontal");
            _forwardInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * _forwardInput * Time.deltaTime * walkSpeed);
            transform.Rotate(Vector3.up * _horizontalInput * Time.deltaTime * turnSpeed);

            // runs script if keys to make elvis move forward or back or horizontal are pressed
            if (_forwardInput != 0 || _horizontalInput != 0)
            {
                // runnung animation is activated
                _animator.SetBool("isrunning", true);
            }

            else
            {
                // running animation is deactivated
                _animator.SetBool("isrunning", false);
            }
            
            // runs script if Space-key is pressed
            if (Input.GetKey("space"))
            {
                //sets to values to dancing
                _isDancing = true;
                _isNotDancing = false;

                // dance animation is ativated
                _animator.SetBool("elvisDance", true);
            }

            else
            {
                // dance animation is deativated
                _animator.SetBool("elvisDance", false);

                // sets values to not dancing
                _isDancing = false;
                _isNotDancing = true;
            }
        }
    }

    // when gameobject Player collides
    private void OnCollisionEnter(Collision collision)
    {
        // Player is Hit while not dancing
        if (collision.gameObject.CompareTag("Enemy") && _isNotDancing)
        {
            // link to Updater
            _updater.UpdateElvis(elvisHit);
        }

        // Player is Hit while dancing
        if (collision.gameObject.CompareTag("Enemy") && _isDancing)
        {
            // link to Updater
            _updater.UpdateFan(fanHit);
        }

        // Player reaches Goal
        if (collision.gameObject.CompareTag("Goal"))
        {
            // link to Updater
            _updater.CheckGameOver(elvisHome = true);

            // calls for method gameOff
            gameOff();
        }
    }
     
    //Ends Game
    public void gameOff()
    {
        // sets value to game over
        _gameOn = false;

    }

    public bool isGameOn()
    {
       //returns value gameOn
       return _gameOn; 
    }
}

