using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : BaseTrigger
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private PlayerInventory inventory;
    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject))
        {
            Debug.Log("Exit");
            if(inventory.GetLoot() > 0){
                gameController.LevelComplete();
            }
        }
    }
}
