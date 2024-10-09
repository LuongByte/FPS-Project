using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    //Current waypoint's index
    public int pointIndex;
    public float waitTime;

    public override void Enter()
    {

    }

    public override void Perform()
    {
        //PatrolCycle();
        if(enemy.PlayerInView()){
            stateMachine.ChangeState(new AttackState());
        }

        if(enemy.ShotHeard()){
            enemy.transform.LookAt(enemy.Player.transform);
            enemy.LastSeen = enemy.Player.transform.position;
            stateMachine.ChangeState(new SearchState());
        }

        PatrolCycle();
    }

    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance){
            waitTime += Time.deltaTime;
            if(waitTime > 2){
                if(pointIndex < enemy.path.waypoints.Count - 1){
                    pointIndex++;
                }
                else{
                    pointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[pointIndex].position);
                waitTime = 0;
            }
        }
    }
}
