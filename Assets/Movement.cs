using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3.0f;

    public Transform floorCheck;
    public float floorDistance = 0.4f;
    public LayerMask floorMask;

    Vector3 velocity;
    bool isOnFloor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOnFloor = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);

        if(isOnFloor && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate vector to move based on player's current position
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if(Input.GetButtonDown("Jump") && isOnFloor)
        {
            // Use equation v.y = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }


        // Because change in vertical distance = (1/2) * g * t^2
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
