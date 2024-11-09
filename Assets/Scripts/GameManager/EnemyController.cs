using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float alertLevel;
    private float alertSpeed;
    [SerializeField]
    private GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        alertLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(alertLevel == 3 && enemies.transform.childCount < 15)
        {
            
        }
    }

    public void updateAlert()
    {
        alertLevel += 1;
    }
}
