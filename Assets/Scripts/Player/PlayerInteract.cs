using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    private InputManager inputManager;
    private bool interacting;
    private float timer;
    private float interactTime;
    [SerializeField] private float rayDis = 3f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TextMeshProUGUI promptMess;
    [SerializeField] private Animator weaponHolder; 
    //private PlayerUI playerUI;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        interacting = false;
        //playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Removes text from screen if not looking at interactable
        promptMess.text = string.Empty;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDis);
        RaycastHit hitInfo;
        //Raycast to center of screen
        if(Physics.Raycast(ray, out hitInfo, rayDis, mask)){
            //Check if rays hits interactable object
            if(hitInfo.collider.GetComponent<Interactable>() != null){
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                //Creates prompt 
                promptMess.text = interactable.promptMessage;
                interactTime = interactable.GetTimer();
                //Time based Interactions
                if(interactTime == 0){
                    if(inputManager.onFoot.Interact.triggered){
                        interactable.BaseInteract();
                    }
                }
                else{
                    if(inputManager.GetInteract()){
                        promptMess.text = string.Empty;
                        interacting = true;
                        weaponHolder.SetBool("Interacting", true);
                        if(timer >= interactTime){
                            interactable.BaseInteract();
                            timer = 0;
                            interacting = false;
                            weaponHolder.SetBool("Interacting", false);
                            inputManager.BuffInteract();
                        }
                        else
                            timer += Time.deltaTime;
                    }
                    else{
                        timer = 0;
                        interacting = false;
                        weaponHolder.SetBool("Interacting", false);
                    }
                }
            }
        }
    }

    public bool CheckInteract()
    {
        return interacting;
    }
}
