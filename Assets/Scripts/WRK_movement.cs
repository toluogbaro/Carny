using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRK_movement : MonoBehaviour
{
    [SerializeField]
    float speed, rotationSpeed, tpsRotationSpeed;
    [SerializeField]
    Vector2 worldAngles, tpsWorldAngles;
    [SerializeField]
    Transform mainCam, tpsCam;
    Rigidbody rb;
    [SerializeField]
    KeyCode switchCamera = KeyCode.None;
    bool isGrounded, jumpReq;

    public int switches;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        switches = 1;

        
    }

    private void Update()
    {
        ChangeCam();
    }

    private void FixedUpdate()
    {
        Movement();
        JumpyPhys();
    }

    private void LateUpdate()
    {
        
        Mousey();
        Jumpy();
    }


    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        Vector3 moveVector = (transform.forward * moveDirection.z) + (transform.right * moveDirection.x);
        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        rb.velocity = new Vector3(moveVector.x * speed, moveVector.y, moveVector.z * speed);

        var camRot = mainCam.eulerAngles;

        transform.rotation = Quaternion.Euler(0, camRot.y, 0);
    }

    private void Mousey()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            worldAngles += rotationSpeed * input;
            tpsWorldAngles += tpsRotationSpeed * input;
        }

        Quaternion lookRotation = Quaternion.Euler(worldAngles);
        Quaternion tpsLookRotation = Quaternion.Euler(tpsWorldAngles);
        mainCam.rotation = lookRotation;
        tpsCam.rotation = tpsLookRotation;


    }

    private void ChangeCam()
    {
        

        if (Input.GetKeyDown(switchCamera))
        {
            Debug.Log("Switch");
            
            if(switches == 1)
            {
                switches++; 
            }
            else
            {
                switches--;
            }

           
           
        }

        if (switches == 1)
        {
            mainCam.gameObject.SetActive(true);
            tpsCam.gameObject.SetActive(false);

        }
        else if (switches == 2)
        {
            mainCam.gameObject.SetActive(false); ;
            tpsCam.gameObject.SetActive(true);
        }
    }

    private void Jumpy()
    {
        RaycastHit hit;
        

        if(Physics.Raycast(transform.position, transform.TransformDirection(0, -1f, 0), out hit, 2f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpReq = true;
        }
        else
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
        
    }

    private void JumpyPhys()
    {
       if(jumpReq)
       {
            rb.AddForce(transform.up *= 200, ForceMode.Impulse);
            rb.drag = 0.5f;

            jumpReq = false;
       }
      
    }


 

}
