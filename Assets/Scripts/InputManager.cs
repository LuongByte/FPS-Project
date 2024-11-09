using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private bool shooting;
    private bool sprinting;
    private bool forward;
    public PlayerInput.OnFootActions onFoot;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        shoot = GetComponent<PlayerShoot>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += SprintPerformed;
        onFoot.Sprint.canceled += SprintCanceled;
        onFoot.Forward.performed += ForwardPerformed;
        onFoot.Forward.canceled += ForwardCanceled;
        onFoot.Shoot.performed += ShootPerformed; 
        onFoot.Shoot.canceled += ShootCanceled; 
        onFoot.Drop.performed += ctx => shoot.Throw(); 
        onFoot.Reload.performed += ctx => shoot.Reload(); 
    }

    public bool GetShoot(){
        return shooting;
    }
    private void ShootPerformed(InputAction.CallbackContext context){
        shooting = true;
    }
    private void ShootCanceled(InputAction.CallbackContext context){
        shooting = false;
    }

    public bool GetSprint(){
        if(shoot.ReloadCheck() || forward == false){
            return false;
        }
        shoot.SprintCheck(sprinting);
        return sprinting;
    }

    private void ForwardPerformed(InputAction.CallbackContext context){
        forward = true;
    }
    private void ForwardCanceled(InputAction.CallbackContext context){
        forward = false;
    }

    private void SprintPerformed(InputAction.CallbackContext context){
        sprinting = true;
    }
    private void SprintCanceled(InputAction.CallbackContext context){
        sprinting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Tells the motor to move with value from movement input
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
