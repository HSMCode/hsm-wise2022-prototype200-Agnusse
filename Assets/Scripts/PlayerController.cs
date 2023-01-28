using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables to move Elvis
    private float horizontalInput;
    private float forwardInput;
    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    // variables to make Elvis dance
    private bool isDancing;
    private bool isNotDancing;

    // player components
    private Animator animator;
    private Rigidbody _playerRb;

    // variables for counting hits
    public int elvisHit = 1;
    public int fanHit = 1;

    // win condition
    public bool elvisHome;

    // to control if the game is still running or over
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

        // get player components
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

            // runs script if keys to make elvis move forward or back or horizontal are pressed
            if (forwardInput != 0 || horizontalInput != 0)
            {
                // runnung animation is activated
                animator.SetBool("isrunning", true);
            }

            else
            {
                // running animation is deactivated
                animator.SetBool("isrunning", false);
            }
            
            // runs script if Space-key is pressed
            if (Input.GetKey("space"))
            {
                //sets to values to dancing
                isDancing = true;
                isNotDancing = false;

                // dance animation is ativated
                animator.SetBool("elvisDance", true);
            }

            else
            {
                // dance animation is deativated
                animator.SetBool("elvisDance", false);

                // sets values to not dancing
                isDancing = false;
                isNotDancing = true;
            }
        }
    }

    // when gameobject Player collides
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

            // calls for method gameOff
            gameOff();
        }
    }
     
    //Ends Game
    public void gameOff()
    {
        // sets value to game over
        gameOn = false;

    }

    public bool isGameOn()
    {
       //returns value gameOn
       return gameOn; 
    }
}

