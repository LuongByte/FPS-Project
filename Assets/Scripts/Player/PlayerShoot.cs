using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private InputManager inputManager;
    private WeaponRecoil recoil;
    private bool inHand, isReloading, isSprinting;
    private Rigidbody rb;
    private BoxCollider coll;
    private float shotTimer;
    private float spread;
    private WeaponStats gunstats;
    private Transform muzzleFlash;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject hole;
    public Transform fpsCam;
    public Transform player;
    public Transform weaponHolder;
    
    public float pushForce = 40f;
    public float dropUpwardForce;
    public float dropForwardForce;
    public Animator holdAnimator;
    public TextMeshProUGUI ammoCount;
    // Start is called before the first frame update
    void Start()
    {
        inHand = true;
        shotTimer = 0f;
        spread = 0f;
        if(inHand == true){
            inHand = true;
            GameObject gun = weaponHolder.transform.GetChild(0).gameObject;
            gunstats = gun.GetComponent<WeaponStats>();
            rb = gunstats.rb;
            coll = gunstats.coll;
            rb.isKinematic = true;
            coll.isTrigger = true;
            holdAnimator.SetBool(gunstats.gunName, true);
            muzzleFlash = gunstats.muzzleFlash;
        }
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        recoil = transform.Find("FPS Cam").GetComponent<WeaponRecoil>();
    }
    void Update()
    {
        if(inputManager.GetShoot() && Time.time >= shotTimer){
            shotTimer = Time.time + 1f/gunstats.fireRate;
            Shoot();
        }
        else{
            if(!(inputManager.GetShoot()) && spread > 0f){
                Debug.Log("SpreadCheck");
                spread -= 0.0005f;
                if((spread - 0.0005f) < 0)
                    spread = 0;
            }
            Idle();
        }
        Debug.Log(spread);
        ammoCount.text = gunstats.currentMagazine.ToString() + "/" + gunstats.currentAmmo.ToString();
    }

    public void Idle()
    {
        if(gunstats.currentMagazine == 0)
            gunstats.gunAnimator.SetBool("Shooting", true);
        else
            gunstats.gunAnimator.SetBool("Shooting", false);
    }
    public void Shoot()
    {
        if(inHand == true && isReloading == false && gunstats.currentMagazine != 0 && isSprinting == false){
            //Calculate Spread
            float xSpread = Random.Range(-spread, spread);
            float ySpread = Random.Range(-spread, spread);
            Vector3 shootDirect = cam.transform.forward + new Vector3(xSpread, ySpread, 0);
            Ray ray = new Ray(cam.transform.position, shootDirect);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, gunstats.range)){
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null){
                    target.takeDamage(gunstats.damage);
                    if(hit.rigidbody != null){
                        hit.rigidbody.AddForce(-hit.normal * pushForce);
                    }
                }
                if(hit.transform.parent != null){
                    if(hit.transform.parent.CompareTag("Wall")){
                        Debug.Log("Wall");
                        GameObject bulletHole = GameObject.Instantiate(Resources.Load("Prefabs/BulletHole") as GameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        bulletHole.transform.position += bulletHole.transform.forward/1000;
                        Destroy(bulletHole, 20);
                    }
                }
            }
            GameObject flash = GameObject.Instantiate(Resources.Load("Prefabs/MuzzleFlash") as GameObject, gunstats.muzzleFlash.position, gunstats.muzzleFlash.rotation);
            flash.transform.SetParent(gunstats.muzzleFlash);
            Destroy(flash, 0.075f);
            gunstats.currentMagazine--;
            gunstats.gunAnimator.SetBool("Shooting", true);
            recoil.Recoil();
            if(spread < gunstats.maxSpread){
                Debug.Log(spread);
                spread += 0.005f;
            }
        }
    }
    
    public void Reload()
    {
        Debug.Log("Reloading");
        if(gunstats.currentAmmo != 0 && gunstats.currentMagazine < gunstats.magazineSize && isSprinting == false){
            StartCoroutine(Reloading());
        }
    }
    
    IEnumerator Reloading(){
        isReloading = true;
        Debug.Log("Reloading");
        holdAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(gunstats.reloadTime + 0.175f);
        holdAnimator.SetBool("Reloading", false);
        isReloading = false;
        if(gunstats.currentAmmo >= gunstats.magazineSize){
            gunstats.currentAmmo -= gunstats.magazineSize - gunstats.currentMagazine;
            gunstats.currentMagazine = gunstats.magazineSize;
        }
        else{
            gunstats.currentMagazine = gunstats.currentAmmo;
            gunstats.currentAmmo = 0;
        }
    }
    
    public void PickUp(GameObject weapon)
    {   
        if(inHand == false){
            inHand = true;
            holdAnimator.SetBool(gunstats.gunName, false);
            int interactlayer = LayerMask.NameToLayer("Default");
            weapon.layer = interactlayer;
            gunstats = weapon.GetComponent<WeaponStats>();
            weapon.transform.SetParent(weaponHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.transform.localScale = Vector3.one;;
            rb = gunstats.rb;
            coll = gunstats.coll;
            rb.isKinematic = true;
            coll.isTrigger = true;
            muzzleFlash = gunstats.muzzleFlash;
            holdAnimator.SetBool(gunstats.gunName, true);
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
    
    public void SprintCheck(bool check){
        isSprinting = check;
    }

    public bool ReloadCheck(){
        return isReloading;
    } 
}
