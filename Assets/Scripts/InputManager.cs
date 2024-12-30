using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInteract interact;
    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private bool interactBuffer;
    private bool shooting, sprinting, interacting, forward;
    [SerializeField]
    private GameController controller;
    public PlayerInput.OnFootActions onFoot;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        interact = GetComponent<PlayerInteract>();
        motor = GetComponent<PlayerMotor>();
        shoot = GetComponent<PlayerShoot>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += SprintPerformed;
        onFoot.Sprint.canceled += SprintCanceled;
        onFoot.Forward.performed += ForwardPerformed;
        onFoot.Forward.canceled += ForwardCanceled;
        onFoot.Interact.performed += InteractPerformed;
        onFoot.Interact.canceled += InteractCanceled;
        onFoot.Shoot.performed += ShootPerformed; 
        onFoot.Shoot.canceled += ShootCanceled; 
        onFoot.Drop.performed += ctx => shoot.Throw(); 
        onFoot.Reload.performed += ctx => shoot.Reload(); 
        onFoot.Pause.performed += ctx =>controller.Pause();
    }

    //Functions to check if button if held down
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
        if(shoot.ReloadCheck() || forward == false || interact.CheckInteract()){
            return false;
        }
        shoot.SprintCheck(sprinting);
        return sprinting;
    }

    public bool GetInteract(){
        if(shoot.ReloadCheck()){
            return false;
        }
        return interacting;
    }

    private void InteractPerformed(InputAction.CallbackContext context){
        if(!interactBuffer)
            interacting = true;
        else
            interacting = false;
    }

    private void InteractCanceled(InputAction.CallbackContext context){
        interacting = false;
        interactBuffer = false;
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
        shoot.SprintCheck(sprinting);
    }

    void FixedUpdate()
    {
        // Tells the motor to move with value from movement input
        if(!interact.CheckInteract())
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        if(!interact.CheckInteract())
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

    public void BuffInteract(){
        interactBuffer = true;
        interacting = false;
    }
}
