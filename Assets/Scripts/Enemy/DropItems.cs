using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    private GameObject cardDrop;
    private Rigidbody rb;
    private BoxCollider coll;
    private Enemy enemy;
    [SerializeField]
    private bool RedCard, YellowCard, BlueCard;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>(); 
        WeaponStats gunstats = enemy.gun.GetComponent<WeaponStats>();
        int InteractableLayer = LayerMask.NameToLayer("Interactable");
        enemy.gun.layer = InteractableLayer;
        rb = gunstats.rb;
        coll = gunstats.coll;
        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    public void Drop()
    {
        enemy.gun.transform.parent = null;
        int InteractableLayer = LayerMask.NameToLayer("Interactable");
        enemy.gun.layer = InteractableLayer;
        enemy.gun.tag = "Drop";
        rb.isKinematic = false;
        coll.isTrigger = false;
        float ranSpin = Random.Range(-1f,1f);
        rb.AddTorque(new Vector3(ranSpin, ranSpin, ranSpin) * 10);

        if(RedCard){
            cardDrop = Instantiate(Resources.Load("Prefabs/RedKeycard") as GameObject);
            cardDrop.transform.position = gameObject.transform.position;
            cardDrop.tag = "Drop";
        }
        else if(YellowCard){
            cardDrop = Instantiate(Resources.Load("Prefabs/YellowKeycard") as GameObject);
            cardDrop.transform.position = gameObject.transform.position;
            cardDrop.tag = "Drop";
        }
        else if(BlueCard){
            cardDrop = Instantiate(Resources.Load("Prefabs/BlueKeycard") as GameObject);
            cardDrop.transform.position = gameObject.transform.position;
            cardDrop.tag = "Drop";
        }
    }
}
