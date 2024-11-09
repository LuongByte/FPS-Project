using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTrigger : BaseTrigger
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private ProgressController progressController;

    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject)){
            progressController.UpdateLoot(inventory.GetLoot());
        }
            
    }
}
