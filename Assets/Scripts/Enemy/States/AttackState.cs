using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{   
    private float moveTimer;
    private float switchTimer;
    private float shootTimer;
    public override void Enter()
    {

    }
    public override void Perform()
    {
        //If player is in view
        if(enemy.PlayerInView()){
            //Lock switch state timer and increment move/shoot timers
            switchTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //Slightly moves enemies around randomly while in combat
            if(moveTimer > Random.Range(3,5)){
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        
            if(shootTimer > enemy.fireRate){
                Shoot();
            }
        }
        //Player not in view
        else{
            switchTimer += Time.deltaTime;
            if(switchTimer > 10){
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunbarrel = enemy.gunBarrel;
        //Create new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //Calculate direction of player
        Vector3 fireDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //Calculate and add force to bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-2f,2f),Vector3.up) * fireDirection * 100;
        Debug.Log("Shoot");
        shootTimer = 0;
    }

    public override void Exit()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
