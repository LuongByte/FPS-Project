using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : BaseTrigger
{
    [SerializeField]
    private PlayerHealth health;
    protected override void OnTriggerStay(Collider collide)
    {
        health.takeDamage(125);
    }
}
