using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromObject : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);


        if (Physics.Raycast(transform.position, down, 2f))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            Debug.Log("hit something in front");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 down = transform.TransformDirection(Vector3.down) * 2;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 2;
        Gizmos.DrawRay(transform.position, fwd);
        Gizmos.DrawRay(transform.position, down);
    }
}
