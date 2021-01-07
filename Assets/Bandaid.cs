using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandaid : MonoBehaviour
{
    public float angleToRotate = 1.0f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1.26f, transform.position.z);
    }

    // Simply rotates bandaid to make it more visually attractive
    void Update()
    {
        transform.Rotate(Vector3.right, angleToRotate);
    }
}
