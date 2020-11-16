using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float smoothTime;

    [SerializeField] GameObject cameraHolder;

    private float verticalLookClamp;
    
    private Vector3 smoothMovement;
    private Vector3 movement;
    Rigidbody rb;
    PhotonView photonView;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if(!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject); // destroy the other players camera for your player (its complicated)
            Destroy(rb);
        }
    }

    private void Update()
    {
        if(!photonView.IsMine) // if the photonview isn't yours
        {
            return;
        }
        CameraRotation();
        Movement();
       
        
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine) // if the photonview isn't yours
        {
            return;
        }
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * Time.fixedDeltaTime);
    }
    private void CameraRotation()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity); // being able to rotate the player around the x axis
        verticalLookClamp += Input.GetAxisRaw("Mouse Y") * mouseSensitivity; // we don't want our player to rotate itself while looking up so we use verticalLookClamp
        verticalLookClamp = Mathf.Clamp(verticalLookClamp, -90f, 90f); // clamp this so you cant look around yourself

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookClamp; // make it so the cameraholder rotates with your own rotation
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; // normalizing this means we won't move faster if we press two keys at once
        movement = Vector3.SmoothDamp(movement, moveDirection * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed), ref smoothMovement, smoothTime); // using a compact if statement, i'm basically telling the script to use the sprintspeed instead of the movespeed when i press down shift, also using smoothdamp
    }
}
