using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : BaseTrigger
{
    [SerializeField]
    private ProgressController controller;
    [SerializeField]
    private PlayerInventory inventory;
    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject))
        {
            Debug.Log("Exit");
            controller.TriggerEscape();
            Destroy(gameObject);
        }
    }
}
