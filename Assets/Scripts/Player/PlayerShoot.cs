using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private InputManager inputManager;
    
    private bool inHand = true;
    private Rigidbody rb;
    private BoxCollider coll;
    [SerializeField]
    private Camera cam;
    public Transform gunBarrel;
    public Transform muzzleFlash;
    public Transform player;
    public Transform weaponHolder;

    public float damage;
    public float range;
    public float pushForce = 40f;
    public float dropUpwardForce;
    public float dropForwardForce;
    public Transform fpsCam;

    
    // Start is called before the first frame update
    void Start()
    {
        inHand = true;
        if(inHand == true){
            GameObject gun = weaponHolder.transform.GetChild(0).gameObject;
            WeaponStats gunstats = gun.GetComponent<WeaponStats>();
            damage = gunstats.damage;
            range = gunstats.range;
            rb = gunstats.rb;
            coll = gunstats.coll;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();

    }

    public void Shoot()
    {
        if(inHand == true){
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, range)){
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null){
                    target.takeDamage(damage);
                    if(hit.rigidbody != null){
                        hit.rigidbody.AddForce(-hit.normal * pushForce);
                    }
                }
            }
            GameObject Flash = GameObject.Instantiate(Resources.Load("Prefabs/MuzzleFlash") as GameObject, muzzleFlash.position, Quaternion.identity);
            Destroy(Flash, 0.1f);
        }

    }
    
    public void Reload(GameObject gun)
    {
        Destroy(gun);
    }

    public void PickUp(GameObject weapon)
    {   
        if(inHand == false){
            inHand = true;
            int interactlayer = LayerMask.NameToLayer("Default");
            weapon.layer = interactlayer;
            weapon.transform.SetParent(weaponHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.transform.localScale = Vector3.one;
            WeaponStats gun = weapon.GetComponent<WeaponStats>();
            damage = gun.damage;
            range = gun.range;
            rb = gun.rb;
            coll = gun.coll;

            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }
    
    public void Throw()
    {
        if(inHand == true){
            inHand = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            GameObject gun = weaponHolder.transform.GetChild(0).gameObject;
            int interactlayer = LayerMask.NameToLayer("Interactable");
            gun.layer = interactlayer;
            weaponHolder.DetachChildren();
            //Add Force when thrown
            rb.velocity = GetComponent<Rigidbody>().velocity;
            rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

            //Spinning
            float ranSpin = Random.Range(-1f,1f);
            rb.AddTorque(new Vector3(ranSpin, ranSpin, ranSpin) * 10);
        }
    }
    
}
