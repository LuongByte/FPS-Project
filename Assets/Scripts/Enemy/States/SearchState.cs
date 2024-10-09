using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer, moveTimer;
    public override void Enter()
    {
        searchTimer = 0;
        moveTimer = 0;
        enemy.Agent.SetDestination(enemy.LastSeen);
    }

    public override void Perform()
    {
        if(enemy.PlayerInView()){
            stateMachine.ChangeState(new AttackState());
        }

        if(enemy.ShotHeard()){
            enemy.transform.LookAt(enemy.Player.transform);
            enemy.LastSeen = enemy.Player.transform.position;
            enemy.Agent.SetDestination(enemy.LastSeen);
            searchTimer = 0;
            moveTimer = 0;
        }


        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance){
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if(searchTimer > 15){
                stateMachine.ChangeState(new PatrolState());
            }
            if(moveTimer > Random.Range(4, 8)){
                moveTimer = 0;
                enemy.Agent.SetDestination(enemy.transform.position + Random.insideUnitSphere * 10 );
            }
        }
    }

    public override void Exit()
    {
        
    }
}
