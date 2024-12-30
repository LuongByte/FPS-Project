using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : Interactable
{

    [SerializeField]private GameObject dynamite;
    [SerializeField]private Fuse fuse;
    void Start()
    {
        dynamite.SetActive(false);
    }

    protected override void Interact()
    {
        dynamite.SetActive(true);
        fuse.UpdateFuse();
        Destroy(gameObject);
    }
}
