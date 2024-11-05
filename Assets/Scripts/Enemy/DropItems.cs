using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    private GameObject cardDrop;
    private Rigidbody rb;
    private BoxCollider coll;
    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private bool RedCard, YellowCard, BlueCard;
    // Start is called before the first frame update
    void Start()
    {
        WeaponStats gunstats = weaponHolder.transform.GetChild(0).GetComponent<WeaponStats>();
        rb = gunstats.rb;
        coll = gunstats.coll;
        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    public void Drop()
    {
        weaponHolder.DetachChildren();
        rb.isKinematic = false;
        coll.isTrigger = false;
        float ranSpin = Random.Range(-1f,1f);
        rb.AddTorque(new Vector3(ranSpin, ranSpin, ranSpin) * 10);

        if(RedCard){
            cardDrop = Instantiate(Resources.Load("Prefabs/RedKeycard") as GameObject);
            
        }
        else if(YellowCard){
            //cardDrop = Instantiate(Resources.Load("Prefabs/RedKeycard") as GameObject);
            Debug.Log("Yellow");
        }
        else if(BlueCard){
            //cardDrop = Instantiate(Resources.Load("Prefabs/RedKeycard") as GameObject);
            Debug.Log("Blue");
        }
        cardDrop.transform.localPosition = gameObject.transform.localPosition;
    }
}
