using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController control;
    private InputManager inputManager;
    private Vector3 playerVelocity;
    private Vector3 keptVelocity;
    private bool isGrounded;
    private bool crouching = false;
    public bool sprinting;
    private bool lerpCrouch = false;
    public float slideTimer;
    private float speed;
    public float baseSpeed;

    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    private float crouchTimer;
    public Animator animator;
    void Start()
    {
        control = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        slideTimer = 0;
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
                if(sprinting == true){
                    control.Move(keptVelocity);
                    keptVelocity = keptVelocity * 0.985f;
                    slideTimer += Time.deltaTime;
                }
                speed = baseSpeed - 5;
            }
            else
                control.height = Mathf.Lerp(control.height, 2, p*2);
            if(p > 1)
            {
                lerpCrouch = false;
            }
        }
        if(inputManager.GetSprint() == true && (slideTimer > 1 || slideTimer == 0) && isGrounded){
            Sprint();
        }
        else{
            Walk();
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirect = Vector3.zero;
        moveDirect.x = input.x;
        moveDirect.z = input.y;
        //Keeps momentum when jumping
        if(isGrounded && (slideTimer > 1 || slideTimer == 0)){
            keptVelocity = transform.TransformDirection(moveDirect) * speed * Time.deltaTime;
            control.Move(keptVelocity);
            
        }
        else{
            control.Move(keptVelocity);
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
            animator.SetBool("Sprinting", false);
        }
    }

    
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        keptVelocity = keptVelocity* 0.75f;
        lerpCrouch = true;
    }
    
    
    public void Sprint()
    {
        if(crouching){
            Crouch();
            slideTimer = 0;
        }
        speed = baseSpeed + 10;
        animator.SetBool("Sprinting", true);
        sprinting = true;
        
    }
    public void Walk()
    {
        if(!crouching){
            speed = baseSpeed;
            sprinting = false;
            slideTimer = 0;
        }
        animator.SetBool("Sprinting", false);
    }
}
