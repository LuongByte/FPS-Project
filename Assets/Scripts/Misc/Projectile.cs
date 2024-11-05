using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private bool exploding;
    [SerializeField]
    private AudioSource impactSound;
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
            DealDamage(hitTransform);
            impactSound.Play();
            if(exploding){

                Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5f);
                GameObject explosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosion1") as GameObject, gameObject.transform.position, Quaternion.identity);
                foreach(Collider c in colliders)
                {
                    Transform splashTransform = c.transform;
                    DealDamage(splashTransform);
                }
                Destroy(explosion, 2f);
            }
            Destroy(gameObject);
        }
    }

    private void DealDamage(Transform hitTransform)
    {
        if(hitTransform.CompareTag("Player")){
            hitTransform.GetComponent<PlayerHealth>().takeDamage(100);
        }
        else if (hitTransform.CompareTag("Target")){
            Debug.Log("Hit Target");
            hitTransform.GetComponent<Target>().TakeDamage(100);
        }
    }
    public void Flying()
    {
        Debug.Log("Flying");
        isFlying = true;
        transform.parent = null;
    }
}
