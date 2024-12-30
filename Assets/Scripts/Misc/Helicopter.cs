using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private ProgressController progressController;
    private float arriveTimer;
    private bool arrived;
    private float health;
    void Start()
    {
        arrived = false;
        arriveTimer = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!arrived){
            if(arriveTimer >= 50f){
                arrived = true;
                progressController.EnableEscape();
            }
            else
                arriveTimer += Time.deltaTime;
        } 
    }

}
