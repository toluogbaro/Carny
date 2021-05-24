using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRK_Jump : MonoBehaviour
{
    #region Variables

    private Rigidbody jumpRB;
    
    private float jumpTimer;
    //Affects how long the player stays in the air after holding jump until they are brought down by gravity
    private bool jumpRequest;
    private bool holdJumpRequest;

    private float veloY;
    private float gravity = -250;


    [SerializeField]
    private KeyCode Jump = KeyCode.None;
    //Set jump key
    [SerializeField]
    private bool isGrounded;


    [Header("JUMP VARIABLES")]

    
    private Vector3 jumpDirection = new Vector3(0, 2.5f, 0);

    [SerializeField]
    private float tapJumpHeight;
    [SerializeField]
    private float holdJumpHeight;
    [SerializeField]
    private float yDrag = default;
    [SerializeField]
    private float jumpTime;
    //jumpTimer is reset to whatever you set jumpTime to. This affects how long you can jump after holding
    //space until you come back down
  


    #endregion

    void Start()
    {
        jumpRB = GetComponent<Rigidbody>();
        
    }


    void Update()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(0, -1f, 0), out raycastHit, 2f))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        //raycast detects if the player is grounded or not by casting ray underneath
        //if raycast detects ground then IsGrounded

        TapJump();

        HoldJump();

        
        

    }

    void FixedUpdate()
    {
        veloY = jumpRB.velocity.y;

        if (jumpRequest)
        {

            jumpRB.AddForce((jumpDirection * tapJumpHeight), ForceMode.Impulse);

            jumpRequest = false;
            holdJumpRequest = false;
        }
        else
        {
            jumpRB.AddForce(Physics.gravity, ForceMode.Acceleration);
        }

        if (holdJumpRequest && jumpTimer > 0)
        {
            jumpRB.AddForce((jumpDirection * holdJumpHeight) * Time.fixedDeltaTime, ForceMode.Impulse);
            jumpTimer -= Time.deltaTime;
            jumpRequest = false;

        }


        if (isGrounded)
        {
            jumpTimer = jumpTime;

        }

        if (jumpRB.velocity.y > 0)
        {
            jumpRB.drag = yDrag;

            if (jumpRB.velocity.y > 0 && jumpRB.velocity.x > 0 || jumpRB.velocity.y > 0 && jumpRB.velocity.z > 0)
            {
                jumpRB.drag = yDrag * 1.5f;
            }
        }
        else
        {
            jumpRB.drag = 0;
        }




    }

    #region Jump Methods

    void TapJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
            //jump animation
        }

       

    }

    void HoldJump()
    {

        if (Input.GetButton("Jump") && jumpTimer > 0)
        {
            holdJumpRequest = true;
        }
        else
        {
            holdJumpRequest = false;
        }

    }

    #endregion
}