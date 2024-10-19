using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController control;
    private InputManager inputManager;
    private Vector3 playerVelocity;
    private Vector3 jumpVelocity;
    private bool isGrounded;
    private bool isSprinting = false;
    private bool crouching = false;
    private bool lerpCrouch = false;
    public float speed = 8f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public float crouchTimer = 1;
    public Animator animator;
    void Start()
    {
        control = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
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
            {
                control.height = Mathf.Lerp(control.height, 1, p*2);
                speed = 3;
            }
            else
                control.height = Mathf.Lerp(control.height, 2, p*2);
            if(p > 1)
            {
                lerpCrouch = false;
            }
        }
        if(inputManager.GetSprint() == true){
            Sprint();
        }
        else if(inputManager.GetSprint() == false){
            Walk();
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirect = Vector3.zero;
        moveDirect.x = input.x;
        moveDirect.z = input.y;
        if(isGrounded){
            jumpVelocity = transform.TransformDirection(moveDirect) * speed * Time.deltaTime;
            control.Move(jumpVelocity);
            
        }
        else{
            control.Move(jumpVelocity);
        }
        playerVelocity.y += 2*(gravity * Time.deltaTime);
        if(isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        control.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * 100);
        }
    }

    
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
        animator.SetBool("Sprinting", false);
    }
    
    
    public void Sprint()
    {
        if(crouching)
            Crouch();
        speed = 12;
        animator.SetBool("Sprinting", true);
        isSprinting = true;
        
    }
    public void Walk()
    {
        if(!crouching)
            speed = 6;
        animator.SetBool("Sprinting", false);
        isSprinting = false;
    
    }
}
