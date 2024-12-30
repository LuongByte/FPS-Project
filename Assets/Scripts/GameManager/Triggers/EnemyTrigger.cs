using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class EnemyTrigger : BaseTrigger
{
    private float stayTimer;
    private float pastTime;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private EnemyController controller;
    void Start()
    {
        stayTimer = 0f;
    }
    void Update()
    {
        if(stayTimer > pastTime){
            pastTime = stayTimer;
        }
        else{
            stayTimer = 0;
            pastTime = 0;
        }

        if(controller.GetAlert() >= 3){

        }
    }
    protected override void OnTriggerEnter(Collider collide)
    {   
        bool res = EnemyCheck(collide.gameObject);
        if(EnemyCheck(collide.gameObject)){
            if(door.GetComponent<Animator>().GetBool("IsOpen") == false){
                door.GetComponent<Animator>().SetBool("IsOpen", true);
            }
        }
    }

    protected override void OnTriggerStay(Collider collide){
        stayTimer += Time.deltaTime;
    }
    protected override void OnTriggerExit(Collider collide)
    {   
        bool res = EnemyCheck(collide.gameObject);
        if(EnemyCheck(collide.gameObject) && controller.GetAlert() < 3){
            if(door.GetComponent<Animator>().GetBool("IsOpen") == true && stayTimer == 0){
                door.GetComponent<Animator>().SetBool("IsOpen", false);
            }
        }
    }
}
