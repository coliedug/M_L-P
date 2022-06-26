using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    Vector3 moveInput;
    public float moveSpeed;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void ProcessMovement()
    {
        if (isLocalPlayer)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        rb.velocity = moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }


}
