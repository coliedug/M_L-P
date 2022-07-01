using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    Vector3 moveInput;
    Vector2 mouseInput;
    public float moveSpeed;
    Rigidbody rb;
    public GameObject playerCamera;
    public float mouseSensitivity = 100;
    float upDownRotation;
    public float maxInteractDistance = 2;
    Vector3 debugHitLocation = new Vector3(0, 0, 0);

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!isLocalPlayer)
        {
            Destroy(playerCamera);
        }
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void Update()
    {
        ProcessMouseMovement();
        ProcessInteraction();
    }

    private void ProcessMovement()
    {
        if (!isLocalPlayer) return;
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;
        rb.velocity = move * moveSpeed * Time.fixedDeltaTime;
    }

    private void ProcessMouseMovement()
    {
        if (!isLocalPlayer) return;
        mouseInput.x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseInput.y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        upDownRotation -= mouseInput.y;
        upDownRotation = Mathf.Clamp(upDownRotation, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(upDownRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x);
    }

    private void ProcessInteraction()
    {
        if (!isLocalPlayer) return;
        //Declares and casts a ray from the player's view to where they are looking then outputs hit data into hit
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxInteractDistance) && hit.collider.gameObject.layer == 6) //checks for a hit object with layer 6 (interactable)
        {
            GameObject hitObject = hit.collider.gameObject;
            debugHitLocation = hit.point;
            GameObject lookedAtInteractable = hit.collider.gameObject;
            // Note: Create onscreen prompt telling the player to press E to interact
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact(hitObject);
            }
        }
    }

    [Command]
    private void Interact(GameObject interactedObject)
    {
        interactedObject.GetComponent<InteractionManager>().Interact();
    }
}
