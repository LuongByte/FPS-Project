using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    //Weapon Name
    public string gunName;
    //Gun Values
    public float damage, reloadTime, fireRate, range, baseSpread, maxSpread;
    //Ammo Values
    public float magazineSize, currentMagazine, maxAmmo, currentAmmo;
    //Recoil Values
    public float recoilX, recoilY, recoilZ;
    public bool silenced, shotgun, automatic, shootProjs;
    public Animator gunAnimator;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform muzzleFlash;
    public AudioSource gunSound;
    public GameObject projectile;
}
