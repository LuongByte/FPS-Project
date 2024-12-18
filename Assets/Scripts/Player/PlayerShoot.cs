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
    private GameObject gun;
    private bool inHand, isReloading, isSprinting;
    private bool noise;
    private Rigidbody rb;
    private BoxCollider coll;
    private float shotTimer, shotCount;
    private float spread;
    private WeaponStats gunstats;
    private CrossHair crosshair;
    private Transform muzzleFlash;
    private Vector3 projSpawn;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject crossha;
    [SerializeField] private GameObject arms;
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
        crosshair = crossha.GetComponent<CrossHair>();
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        recoil = transform.Find("FPS Cam").GetComponent<WeaponRecoil>();
        arms.SetActive(false);
        shotTimer = 0f;
        spread = 0f;
        if(weaponHolder.childCount > 1)
            inHand = true;
        if(inHand == true){
            inHand = true;
            arms.SetActive(true);
            GameObject gun = weaponHolder.transform.GetChild(0).gameObject;
            int interactlayer = LayerMask.NameToLayer("Default");
            gun.layer = interactlayer;
            gun.tag = "Untagged";
            gunstats = gun.GetComponent<WeaponStats>();
            rb = gunstats.rb;
            coll = gunstats.coll;
            rb.isKinematic = true;
            coll.isTrigger = true;
            coll.enabled = false;
            holdAnimator.SetBool(gunstats.gunName, true);
            muzzleFlash = gunstats.muzzleFlash;
            spread = gunstats.baseSpread;
            crosshair.setBase(10 + gunstats.baseSpread * 100);
            if(gunstats.shotgun)
                shotCount = 8;
            else
                shotCount = 1;
        }
    }
    void Update()
    {   
        if(inHand == true)
        {
            if(inputManager.GetShoot() && Time.time >= shotTimer && !inputManager.GetInteract()){
                shotTimer = Time.time + 1f/gunstats.fireRate;
                Shoot();
            }
            else{
                if(!(inputManager.GetShoot()) && spread > gunstats.baseSpread){
                    spread -= 0.0005f;
                    crosshair.decVal(10);
                    if((spread - 0.0005f) < gunstats.baseSpread){
                        spread = gunstats.baseSpread;
                    }
                }
                Idle();
                noise = false;

            }
            ammoCount.text = gunstats.currentMagazine.ToString() + "/" + gunstats.currentAmmo.ToString();
        }
        else{
            ammoCount.text = "";
        }
    }

    public void Idle()
    {
        holdAnimator.SetBool("Kicking", false);
        if(gunstats.currentMagazine == 0)
            gunstats.gunAnimator.SetBool("Shooting", true);
        else
            gunstats.gunAnimator.SetBool("Shooting", false);
    }
    public void Shoot()
    {
        if(inHand == true && isReloading == false && gunstats.currentMagazine != 0 && isSprinting == false){
            if(gunstats.shootProjs)
            {
                ShootProjectile();
            
            }
            else
                ShootHitScan();
            if(!gunstats.silenced){
                GameObject flash = Instantiate(Resources.Load("Prefabs/MuzzleFlash") as GameObject, gunstats.muzzleFlash.position, gunstats.muzzleFlash.rotation);
                flash.transform.SetParent(gunstats.muzzleFlash);
                Destroy(flash, 0.075f);
                noise = true;
            }

            if(gunstats.gunSound.isPlaying){
                gunstats.gunSound.Stop();
                gunstats.gunSound.Play();
            }
            else
                gunstats.gunSound.Play();
                
            gunstats.currentMagazine--;
            gunstats.gunAnimator.SetBool("Shooting", true);
            holdAnimator.SetBool("Kicking", true);
            recoil.Recoil(gunstats.recoilX, gunstats.recoilY, gunstats.recoilZ);
            if(spread < gunstats.maxSpread){
                spread += 0.005f;
                crosshair.incVal(4);
            }
        }
    }

        public void ShootHitScan()
        {
            for(int i = 0; i < shotCount; i++){
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
                        target.TakeDamage(gunstats.damage);
                        if(hit.rigidbody != null){
                            hit.rigidbody.AddForce(-hit.normal * pushForce);
                        }
                    }
                    else{
                        if(hit.transform != null){
                            if(hit.transform.CompareTag("Wall")){
                                Debug.Log("Wall");
                                GameObject bulletHole = Instantiate(Resources.Load("Prefabs/BulletHole") as GameObject, hit.point, Quaternion.LookRotation(hit.normal));
                                bulletHole.transform.position += bulletHole.transform.forward/1000;
                                Destroy(bulletHole, 20);
                            }
                            else{
                                if(hit.transform.parent != null){
                                    if(hit.transform.parent.CompareTag("Wall")){
                                        Debug.Log("Wall");
                                        GameObject bulletHole = Instantiate(Resources.Load("Prefabs/BulletHole") as GameObject, hit.point, Quaternion.LookRotation(hit.normal));
                                        bulletHole.transform.position += bulletHole.transform.forward/1000;
                                        Destroy(bulletHole, 20);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ShootProjectile()
        {
            projSpawn = gunstats.projectile.transform.localPosition;
            Projectile proj = gunstats.projectile.GetComponent<Projectile>();
            proj.Flying();
            Vector3 fireDirection = -gunstats.projectile.transform.forward;
            Rigidbody prb = gunstats.projectile.GetComponent<Rigidbody>();
            prb.isKinematic = false;
            prb.velocity = fireDirection * 70;
        }
    
    public bool makeNoise()
    {
        return noise;
    }

    
    public void Reload()
    {
        if(gunstats.currentAmmo != 0 && gunstats.currentMagazine < gunstats.magazineSize && isSprinting == false && !inputManager.GetInteract()){
            StartCoroutine(Reloading());
        }
    }
    
    IEnumerator Reloading(){
        isReloading = true;
        holdAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(gunstats.reloadTime + 0.175f);
        holdAnimator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.15f);
        isReloading = false;
        if(gunstats.currentAmmo >= gunstats.magazineSize){
            gunstats.currentAmmo -= gunstats.magazineSize - gunstats.currentMagazine;
            gunstats.currentMagazine = gunstats.magazineSize;
        }
        else{
            gunstats.currentMagazine = gunstats.currentAmmo;
            gunstats.currentAmmo = 0;
        }

        if(gunstats.shootProjs){
            gunstats.projectile = GameObject.Instantiate(Resources.Load("Prefabs/Rocket") as GameObject);
            gunstats.projectile.transform.SetParent(gun.transform);
            gunstats.projectile.transform.localPosition = projSpawn;
            gunstats.projectile.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gunstats.projectile.transform.localScale = Vector3.one;
        }
    }
    
    public void PickUp(GameObject weapon)
    {   
        if(inHand == false){
            gun = weapon;
            inHand = true;
            arms.SetActive(true);
            int interactlayer = LayerMask.NameToLayer("Default");
            weapon.layer = interactlayer;
            gun.tag = "Untagged";
            gunstats = weapon.GetComponent<WeaponStats>();
            weapon.transform.SetParent(weaponHolder);
            weapon.transform.SetSiblingIndex(0);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.transform.localScale = Vector3.one;
            rb = gunstats.rb;
            coll = gunstats.coll;
            rb.isKinematic = true;
            coll.isTrigger = true;
            coll.enabled = false;
            muzzleFlash = gunstats.muzzleFlash;
            holdAnimator.SetBool(gunstats.gunName, true);
            spread = gunstats.baseSpread;
            crosshair.setBase(10 + gunstats.baseSpread * 500);
            if(gunstats.shotgun)
                shotCount = 8;
            else
                shotCount = 1;
        }
    }
    
    public void Throw()
    {
        if(inHand == true){
            inHand = false;
            arms.SetActive(false);
            rb.isKinematic = false;
            coll.isTrigger = false;
            coll.enabled = true;
            holdAnimator.SetBool(gunstats.gunName, false);
            GameObject gun = weaponHolder.transform.GetChild(0).gameObject;
            int interactlayer = LayerMask.NameToLayer("Interactable");
            gun.layer = interactlayer;
            gun.tag = "Drop";
            gun.transform.parent = null;
            gun.transform.localScale = new Vector3(5f, 5f, 5f);
            //Add Force when thrown
            rb.velocity = GetComponent<Rigidbody>().velocity;
            rb.velocity = fpsCam.forward * dropForwardForce;
            rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
            //Spinning
            float ranSpin = Random.Range(-1f,1f);
            rb.AddTorque(new Vector3(ranSpin, ranSpin, ranSpin) * 10);
            ammoCount.text = string.Empty;
        }
    }
    
    public void SprintCheck(bool check){
        isSprinting = check;
    }

    public bool ReloadCheck(){
        
        return isReloading;
    } 
}
