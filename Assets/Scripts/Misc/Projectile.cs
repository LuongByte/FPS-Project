using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private bool exploding;
    [SerializeField]
    private AudioSource flyingSound;
    private bool isFlying;
    private float timer;
    private Explosion explosion;
    void Start()
    {
        isFlying = false;
        if(exploding == true)
            explosion = GetComponent<Explosion>();
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
            if(exploding == true){
                Transform hitTransform = collision.transform;
                DealDamage(hitTransform);
                explosion.Explode(gameObject.transform.position, 10f, "Explosion1");
            }
            else
                Destroy(gameObject);
        }
    }

    private void DealDamage(Transform hitTransform)
    {
        if(hitTransform.CompareTag("Player")){
            hitTransform.GetComponent<PlayerHealth>().takeDamage(100);
        }
        else if (hitTransform.CompareTag("Target")){
            hitTransform.GetComponent<Target>().TakeDamage(100);
        }
    }
    public void Flying()
    {
        isFlying = true;
        flyingSound.Play();
        transform.parent = null;
    }
}
