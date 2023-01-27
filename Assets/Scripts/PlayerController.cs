using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables for movement
    // private float horizontalInput;
    // private float forwardInput;


    private bool gameOn;

    // private MoveFan _MoveFan;

    // [SerializeField] float turnSpeed;
    // [SerializeField] float walkSpeed;
    // [SerializeField] float runSpeed;



    // dancing
    private bool isDancing;
    private bool isNotDancing;

    // player components
    //private Animator _playerAnim;
    private Rigidbody _playerRb;

    //variables for counting hits
    public int elvisHit = 1;
    public int fanHit = 1;

    private Updater _updater;

    // win condition
    public bool elvisHome;

    //private Animator animator;

    //private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gameOn = true;
        // _MoveFan = GameObject.FindWithTag("Enemy").GetComponent<MoveFan>();
        //animator = GetComponent<Animator>();
        //_playerAnim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody>();

        // link to Updater
        _updater = GameObject.Find("Updater").GetComponent<Updater>();

        isDancing = false;
        elvisHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
            //     // WALKING and RUNNING
            // horizontalInput = Input.GetAxis("Horizontal");
            // forwardInput = Input.GetAxis("Vertical");

            // transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * walkSpeed);
            // transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnSpeed);

            // // walking animation
            // if (forwardInput != 0 || horizontalInput != 0)
            // {
            //     //_playerAnim.SetBool("Walk", true);
            // }

            // else
            // {
            //     //_playerAnim.SetBool("Walk", false);
            // }

            // // running animation and more speed
            // if (forwardInput != 0 && Input.GetKey(KeyCode.LeftShift))
            // {
            //     //_playerAnim.SetBool("Run", true);
            //     transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * runSpeed);
            // }

            // else
            // {
            //     //_playerAnim.SetBool("Run", false);
            // }

            // DANCING
            if (Input.GetKey("space"))
            {
                isDancing = true;
                isNotDancing = false;
                //animator.SetBool("elvisDance", true);
                print("Elvis uses cool dance move!");
            }

            else
            {
                //animator.SetBool("elvisDance", false);
                isDancing = false;
                isNotDancing = true;
            }
        }
    }
    // COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        // Player is Hit
        if (collision.gameObject.CompareTag("Enemy") && isNotDancing)
        {
            print("Elvis was hit!");

            // link to Updater
            _updater.UpdateElvis(elvisHit);
        }

        // Player fights Enemy
        if (collision.gameObject.CompareTag("Enemy") && isDancing)
        {
            print("Fan was hit!");

            // link to Updater
            _updater.UpdateFan(fanHit);
        }

        // Player reaches Goal
        if (collision.gameObject.CompareTag("Goal"))
        {
            _updater.CheckGameOver(elvisHome = true);

            print("Elvis is safe now!");

            gameOff();
            // _MoveFan.gameOff();
            
        }
    }
     public void gameOff()
    {
        gameOn = false;

    }

    public bool isGameOn()
    {
       return gameOn; 
    }
}

