using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCheck : MonoBehaviour
{
    public bool debugAcceleration;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (debugAcceleration) Debug.Log(rb.velocity);
    }
}
