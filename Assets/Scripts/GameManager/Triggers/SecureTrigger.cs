using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SecureTrigger : BaseTrigger
{
    private int count;
    private float maxLoot;
    private float index;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private ProgressController progressController;
    [SerializeField] private GameObject gold;
    [SerializeField] private float value;
    void Start(){
        count = gold.transform.childCount;
        GameObject child;
        for(int i = 0; i < count; i++){
            child = gold.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
        count = 0;
    }
    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject)){
            float loot = inventory.GetLoot();
            if(loot != 0){
                index = loot / value;
                progressController.UpdateLoot(loot);
                GameObject child;
                for(int i = 0; i < index; i++){
                    child = gold.transform.GetChild(count).gameObject;
                    child.SetActive(true);
                    count++;
                }
            }
        }
            
    }
}
