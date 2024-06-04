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
        if(enemy.PlayerInView()){
            switchTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            if(moveTimer > Random.Range(3,5)){
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else{
            switchTimer += Time.deltaTime;
            if(switchTimer > 10){
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
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
