using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController control;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public bool isSprinting = false;
    public bool crouching = false;
    public bool lerpCrouch = false;
    public float speed = 8f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public float crouchTimer = 1;

    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = control.isGrounded;
        
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p*=p;
            if(crouching)
                control.height = Mathf.Lerp(control.height, 1, p);
            else
                control.height = Mathf.Lerp(control.height, 2, p);
            if(p > 1)
            {
                lerpCrouch = false;
            }
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirect = Vector3.zero;
        moveDirect.x = input.x;
        moveDirect.z = input.y;
        control.Move(transform.TransformDirection(moveDirect) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        control.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    
    
    public void Sprint()
    {
        isSprinting = !isSprinting;
        if(isSprinting)
            speed = 15;
        else
            speed = 8;
        
    }

}
