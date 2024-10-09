using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private bool exploding;
    private bool isFlying;
    private float timer;
    void Start()
    {
        isFlying = false;
    }

    void Update()
    {
        if(isFlying){
            timer += Time.deltaTime;
            if(timer >= 7){
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isFlying){
            Transform hitTransform = collision.transform;
            DamageDeal(hitTransform);
            
            if(exploding){

                Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5f);
                GameObject explosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, hitTransform.position, hitTransform.rotation);
                foreach(Collider c in colliders)
                {
                    Debug.Log("Splash");
                    Transform splashTransform = c.transform;
                    DamageDeal(splashTransform);
                }
                Destroy(explosion, 2f);
            }
            Destroy(gameObject);
        }
    }

    private void DamageDeal(Transform hitTransform)
    {
        if(hitTransform.CompareTag("Player")){
            hitTransform.GetComponent<PlayerHealth>().takeDamage(100);
        }
        else if (hitTransform.CompareTag("Target")){
            Debug.Log("Hit Target");
            hitTransform.GetComponent<Target>().takeDamage(100);
        }
    }
    public void Flying()
    {
        Debug.Log("Flying");
        isFlying = true;
        transform.parent = null;
    }
}
