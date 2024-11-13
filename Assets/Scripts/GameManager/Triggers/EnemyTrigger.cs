using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class EnemyTrigger : BaseTrigger
{
    private float closeTimer;
    [SerializeField]
    private GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        closeTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(door.GetComponent<Animator>().GetBool("IsOpen") == true){
            if(closeTimer > 5f){
                door.GetComponent<Animator>().SetBool("IsOpen", false);
                closeTimer = 0;
            }
            else
                closeTimer += Time.deltaTime;
        }
        
    }

    protected override void OnTriggerEnter(Collider collide)
    {   
        bool res = EnemyCheck(collide.gameObject);
        Debug.Log("Check");
        if(EnemyCheck(collide.gameObject)){
            if(door.GetComponent<Animator>().GetBool("IsOpen") == false){
                door.GetComponent<Animator>().SetBool("IsOpen", true);
            }
            closeTimer = 0;
        }
    }
}
