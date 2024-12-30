using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer, moveTimer, posiTimer;
    private float posiDelay;
    public override void Enter()
    {
        searchTimer = 0;
        moveTimer = 0;
        enemy.Agent.SetDestination(enemy.LastSeen);
    }

    public override void Perform()
    {
        if(enemy.PlayerInView()){
            stateMachine.ChangeState(new ShockState());
        }
        if(enemy.DamageFelt()){
            enemy.ResetDamage();
            stateMachine.ChangeState(new ShockState());
        }
        if(enemy.controller.GetAlert() < 3){
            if(enemy.ShotHeard()){
                enemy.Agent.SetDestination(enemy.LastSeen);
                searchTimer = 0;
                moveTimer = 0;
            }
            if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance){
                searchTimer += Time.deltaTime;
                moveTimer += Time.deltaTime;
                if(searchTimer > 10f){
                    stateMachine.ChangeState(new PatrolState());
                }
                if(moveTimer > Random.Range(4, 8)){
                    moveTimer = 0;
                    enemy.Agent.SetDestination(enemy.transform.position + Random.insideUnitSphere * 10 );
                }
            }
        }
        else{
            enemy.Agent.SetDestination(enemy.LastSeen);
        }
        
    }

    public override void Exit()
    {
        
    }
}
