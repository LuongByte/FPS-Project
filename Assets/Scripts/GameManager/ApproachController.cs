using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ApproachController : MonoBehaviour
{
    private ProgressController progController;
    [SerializeField]
    private GameObject stealthEscape, loudEscape, stealthSecure, loudSecure, stealthCall, loudCall;
    [SerializeField] private Interactable stealthObj, loudObj;
    private bool loud;
    public int cutOff;
    // Start is called before the first frame update
    void Start()
    {
        progController = GetComponent<ProgressController>();
        loudEscape.SetActive(false);
        stealthEscape.SetActive(false);
        loudSecure.SetActive(false);
        stealthCall.GetComponent<Interactable>().DisableInteract();
        loudCall.GetComponent<Interactable>().DisableInteract();
        loudObj.DisableInteract();
        loudCall.SetActive(false);
        loud = false;
    }

    public void SwitchApproach()
    {
        progController.AlarmRaised(cutOff);
        if(cutOff >= progController.GetProgress()){
            if(cutOff == progController.GetProgress()){
                Debug.Log("minus one");
                progController.UpdateProgress(-1);
            }
            if(cutOff - 1 == progController.GetProgress()){
                progController.UpdateProgress(0);
                Debug.Log("zero");
            }
            stealthSecure.SetActive(false);
            loudSecure.SetActive(true);
            stealthObj.DisableInteract();
            loudObj.EnableInteract();
            loud = true;
        }
    }

    public GameObject GetEscape()
    {
        if(loud)
            return loudEscape;
        else
            return stealthEscape;   
    }

    public GameObject GetEscapeCall()
    {
        if(loud){
            loudCall.SetActive(true);
            loudCall.GetComponent<Interactable>().EnableInteract();
            return loudCall;
        }
        else{
            stealthCall.GetComponent<Interactable>().EnableInteract();
            return stealthCall;  
        }
    }
}
