using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public float angleToRotate = 1.0f;
    // Simply rotates rifle to make it more visually attractive
    void Update()
    {
        transform.Rotate(Vector3.up, angleToRotate);
    }
}
