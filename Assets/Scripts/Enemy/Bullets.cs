using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision){
        Transform hitTransform = collision.transform;
        if(hitTransform.CompareTag("Player")){
            //Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>().takeDamage(100);
            Destroy(gameObject);
        }
        else if (hitTransform.CompareTag("Target")){
            //Debug.Log("Hit Target");
            Destroy(gameObject);
            hitTransform.GetComponent<Target>().takeDamage(100);
        }
        else{
            //Debug.Log(hitTransform.transform.name);
            Destroy(gameObject, 5);
        }
    }
}
