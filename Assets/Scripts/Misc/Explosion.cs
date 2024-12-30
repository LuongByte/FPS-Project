using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode(Vector3 position, float size, string fileName)
    {
        Collider[] colliders = Physics.OverlapSphere(position, size);
        GameObject explosion = GameObject.Instantiate(Resources.Load("Prefabs/" + fileName) as GameObject, position, Quaternion.identity);
        foreach(Collider c in colliders)
        {
            Transform splashTransform = c.transform;
            DealDamage(splashTransform);
        }
        AudioSource explosionSound = explosion.GetComponent<AudioSource>();
        explosionSound.Play();
        Destroy(explosion, 5f);
        Destroy(gameObject);
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
}
