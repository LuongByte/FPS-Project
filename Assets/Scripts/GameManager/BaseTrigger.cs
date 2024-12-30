using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    public bool PlayerCheck(GameObject gameObject)
    {
        return gameObject.CompareTag("Player");
    }
    public bool EnemyCheck(GameObject gameObject)
    {
        if(gameObject.CompareTag("Player") || gameObject.CompareTag("Drop"))
            return false;
        else
            return true;
    }
    protected virtual void OnTriggerEnter(Collider collide)
    {

    }

    protected virtual void OnTriggerStay(Collider collide){
        
    }
    protected virtual void OnTriggerExit(Collider collide)
    {

    }
}
