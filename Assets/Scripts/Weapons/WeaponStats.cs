using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float recoilX, recoilY, recoilZ;
    //Weapon Name
    public string gunName;
    //Gun Values
    public float damage, reloadTime, fireRate, range, maxSpread;
    //Ammo Values
    public float magazineSize, currentMagazine, maxAmmo, currentAmmo;
    //Recoil Values
    public Animator gunAnimator;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform muzzleFlash;
}
