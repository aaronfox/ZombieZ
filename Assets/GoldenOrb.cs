using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenOrb : MonoBehaviour
{
    public float maxSpeed = 0.1f;
    private float startPositionY;

    private void Start()
    {
        startPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Bounce up and down and spin
        transform.Rotate(Vector3.up, 3);
        transform.position = new Vector3(transform.position.x, startPositionY + Mathf.Sin(Time.time * maxSpeed) + 0.5f, transform.position.z);
    }
}
