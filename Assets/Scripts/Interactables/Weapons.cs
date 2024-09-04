using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    public GameObject pickUp;
    private PlayerShoot shoot;

    protected override void Interact()
    {
        shoot = FindObjectOfType<PlayerShoot>();
        shoot.PickUp(pickUp);
    } 
}
