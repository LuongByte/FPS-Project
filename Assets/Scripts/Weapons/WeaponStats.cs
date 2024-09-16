using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    //Weapon Name
    public string gunName;
    //Gun Values
    public float damage, reloadTime, fireRate, range, maxSpread;
    //Ammo Values
    public float magazineSize, currentMagazine, maxAmmo, currentAmmo;
    //Recoil Values
    private float recoilX, recoilY, recoilZ;
    public Animator gunAnimator;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform muzzleFlash;
}
