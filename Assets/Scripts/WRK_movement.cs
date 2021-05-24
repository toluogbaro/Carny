using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRK_movement : MonoBehaviour
{
    [SerializeField]
    float speed, rotationSpeed;
    [SerializeField]
    Vector2 worldAngles;
    [SerializeField]
    Transform cam, player;
    Rigidbody rb;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
        
    }

    private void LateUpdate()
    {
        
        Mousey();
    }


    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        Vector3 moveVector = (transform.forward * moveDirection.z) + (transform.right * moveDirection.x);

        rb.velocity = new Vector3(moveVector.x * speed, moveVector.y, moveVector.z * speed);

        var camRot = cam.eulerAngles;

        transform.rotation = Quaternion.Euler(0, camRot.y, 0);
    }

    private void Mousey()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            worldAngles += rotationSpeed * input;
        }

        Quaternion lookRotation = Quaternion.Euler(worldAngles);
        cam.rotation = lookRotation;
    }




 

}
