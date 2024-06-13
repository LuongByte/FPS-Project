using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision){
        Transform hitTransform = collision.transform;
        if(hitTransform.CompareTag("Player")){
            Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>().takeDamage(10);
            Destroy(gameObject);
        }
        else{
            Destroy(gameObject, 5);
        }
    }
}
