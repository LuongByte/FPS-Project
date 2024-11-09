using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    public bool PlayerCheck(GameObject gameObject)
    {
        return gameObject.CompareTag("Player");
    }
    protected virtual void OnTriggerEnter(Collider collide)
    {

    }
}
