using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //moving
    private float horizontalInput;
    private float forwardInput;
    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    // dancing
    private bool isDancing;
    private bool isNotDancing;

    // player components
    private Animator animator;
    private Rigidbody _playerRb;

    //variables for counting hits
    public int elvisHit = 1;
    public int fanHit = 1;

    // win condition
    public bool elvisHome;

    //Game is played/not GameOver condition
    private bool gameOn;

    //link to Updater-Script
    private Updater _updater;
    
    // Start is called before the first frame update
    void Start()
    {
        //Starts the game
        gameOn = true;

        //sets to basic value not dancing
        isDancing = false;

        //sets the goal to not reached
        elvisHome = false;

        // find particle systems
        _playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //find Updater-Script
        _updater = GameObject.Find("Updater").GetComponent<Updater>();
    }

    // Update is called once per frame
    void Update()
    {
        //runs the main script while GameOn
        if (gameOn)
        {
            //Elvis moves
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * walkSpeed);
            transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnSpeed);

            // running animation
            if (forwardInput != 0 || horizontalInput != 0)
            {
                animator.SetBool("isrunning", true);
            }

            else
            {
                animator.SetBool("isrunning", false);
            }
            
            // DANCING
            if (Input.GetKey("space"))
            {
                isDancing = true;
                isNotDancing = false;
                animator.SetBool("elvisDance", true);
            }

            else
            {
                animator.SetBool("elvisDance", false);
                isDancing = false;
                isNotDancing = true;
            }
        }
    }

    // COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        // Player is Hit while not dancing
        if (collision.gameObject.CompareTag("Enemy") && isNotDancing)
        {
            // link to Updater
            _updater.UpdateElvis(elvisHit);
        }

        // Player is Hit while dancing
        if (collision.gameObject.CompareTag("Enemy") && isDancing)
        {
            // link to Updater
            _updater.UpdateFan(fanHit);
        }

        // Player reaches Goal
        if (collision.gameObject.CompareTag("Goal"))
        {
            // link to Updater
            _updater.CheckGameOver(elvisHome = true);
            gameOff();
        }
    }
     
    //Ends Game
    public void gameOff()
    {
        gameOn = false;

    }

    //restarts Game
    public bool isGameOn()
    {
       return gameOn; 
    }
}

