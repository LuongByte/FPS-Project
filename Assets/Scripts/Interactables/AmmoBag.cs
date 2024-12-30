using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class AmmoBag : Interactable
{
    [SerializeField]
    private PlayerShoot playerShoot;
    [SerializeField]
    private float refillPercent;
    protected override void Interact()
    {
        playerShoot.Refill(refillPercent);
    }
}
