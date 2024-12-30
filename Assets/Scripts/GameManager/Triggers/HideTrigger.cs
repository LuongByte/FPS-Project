using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : BaseTrigger
{
    private float stayTimer;
    
    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject))
        {
            Debug.Log("Hiding");
            stayTimer = 0;
        }
    }

    protected override void OnTriggerStay(Collider collide){
        stayTimer += Time.deltaTime;
    }

}
