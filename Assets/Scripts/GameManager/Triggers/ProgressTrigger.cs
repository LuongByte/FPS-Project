using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTrigger : BaseTrigger
{

    [SerializeField] private ProgressController progressController;
    [SerializeField] public string progressMessage;


    protected override void OnTriggerEnter(Collider collide)
    {
        if(PlayerCheck(collide.gameObject))
            progressController.UpdateProgress(gameObject, progressMessage);
    }
}
