using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    private InputManager inputManager;
    [SerializeField] private float rayDis = 3f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TextMeshProUGUI promptMess;
    //private PlayerUI playerUI;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        //playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Removes text from screen if not looking at interactable
        //playerUI.UpdateText(string.Empty);
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
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
