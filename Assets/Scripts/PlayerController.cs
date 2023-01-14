using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables for movement
    private float horizontalInput;
    private float forwardInput;

    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    public float force;
    public float gravityModifier = 1f;

    public bool isGrounded;
    public bool isJumping;
    public bool isFalling;
    public bool isLanding;
    public bool jumpCancelled;
    public float jumpTimer;
    public float jumpButtonPressedTime = 1f;


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
        // WALKING and RUNNING
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * walkSpeed);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnSpeed);

        // walking animation
        if (forwardInput != 0 || horizontalInput != 0)
        {
            //_playerAnim.SetBool("Walk", true);
        }

        else
        {
            //_playerAnim.SetBool("Walk", false);
        }

        // running animation and more speed
        if (forwardInput != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            //_playerAnim.SetBool("Run", true);
            transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * runSpeed);
        }

        else
        {
            //_playerAnim.SetBool("Run", false);
        }

        // press space to jump - player is JUMPING
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            isGrounded = false;
            isJumping = true;

            if (isJumping)
            {
                //_playerAnim.SetTrigger("Jump");
            }
        }

        // release space to start falling - player is falling
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                isFalling = true;

                if (isFalling)
                {
                    //_playerAnim.SetBool("Fall", true);
                }
            }

            if (jumpTimer > jumpButtonPressedTime)
            {
                isJumping = false;
                jumpCancelled = true;
            }
        }

        if (_playerRb.velocity.y < 0 && isFalling)
        {
            isFalling = false;
            isLanding = true;
            //_playerAnim.SetBool("Fall", false);
        }

        // DANCING
        if (Input.GetKey("p"))
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

    void FixedUpdate()
    {
        if (isJumping)
        {
            gravityModifier = 1f;
            _playerRb.AddForce(Vector3.up * force, ForceMode.Force);
        }

        if (isFalling || isGrounded || isLanding || jumpCancelled)
        {
            gravityModifier = 25f;
        }

        _playerRb.AddForce(Physics.gravity * (gravityModifier - 1) * _playerRb.mass);
    }

    // COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        // Player is on Ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpTimer = 0f;
            jumpCancelled = false;

            if (isLanding)
            {
                //_playerAnim.SetBool("Land", false);
                isLanding = false;
            }
        }

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
        }
    }
}

