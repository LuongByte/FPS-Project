using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    //Recoil Rotations
    private Vector3 currentRotate;
    private Vector3 targetRotate;
    [SerializeField]
    private float snappiness, returnSpeed;
    // Update is called once per frame
    void Update()
    {
        //Calculate and apply recoil
        targetRotate = Vector3.Lerp(targetRotate, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotate = Vector3.Slerp(currentRotate, targetRotate, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotate);
    }

    public void Recoil(float recoilX, float recoilY, float recoilZ)
    {
        targetRotate += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
