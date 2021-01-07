using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // How sensitive mouser movement is
    public float sensitivity = 100f;

    // Reference to player object that is being used as FPS
    public Transform player;

    // Keeps track of rotation on the X axis
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Hide and lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation = xRotation - mouseY;
        // Clamp rotation to ensure that player does not look unrealistically or flip around
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(new Vector3(0, 1, 0) * mouseX);
    }
}
