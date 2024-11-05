using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Inventory inventory;
    void OnTriggerEnter()
    {
        Debug.Log("Exit");
        if(inventory.GetLoot() > 0){
            gameController.LevelComplete();
        }
    }
}
