using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ProgressTrigger : BaseTrigger
{
    [SerializeField] private ProgressController progressController;
    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject)){
            progressController.UpdateProgress(1);
            Destroy(gameObject);
        }
    }
}
