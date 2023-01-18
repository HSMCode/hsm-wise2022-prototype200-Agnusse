using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elvisDance : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;

    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * walkSpeed);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnSpeed);

        // walking animation
        if (forwardInput != 0 || horizontalInput != 0)
        {
            animator.SetBool("isrunning", true);
        }

        else
        {
            animator.SetBool("isrunning", false);
        }
        



        //Dance Moves
        if (Input.GetKey("p"))
        {
            animator.SetBool("elvisDance", true);
        }

        else
        {
            animator.SetBool("elvisDance", false);
        }
    }
}
