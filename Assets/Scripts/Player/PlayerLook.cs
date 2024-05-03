using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float xRotate = 0f;

    public Camera cam;
    public float xSensitive = 30f;
    public float ySensitive = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculates camera rotation from input
        xRotate -= (mouseY * Time.deltaTime) * ySensitive;
        //Sets range of xRotate
        xRotate = Mathf.Clamp(xRotate, -80f, 80f);
        //Apply calculation to transform camera
        cam.transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
        //
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitive);
    }
}
