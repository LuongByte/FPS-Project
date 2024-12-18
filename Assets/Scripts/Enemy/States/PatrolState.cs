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
        pointIndex = 0;
    }

    public override void Perform()
    {
        if(enemy.controller.GetAlert() < 3){
            if(enemy.PlayerInView()){
                stateMachine.ChangeState(new ShockState());
            }

            if(enemy.ShotHeard()){
                enemy.LastSeen = enemy.Player.transform.position;
                stateMachine.ChangeState(new ShockState());
            }
            if(enemy.DamageFelt()){
                enemy.ResetDamage();
                stateMachine.ChangeState(new ShockState());
            }
            PatrolCycle();
        }
        else
            stateMachine.ChangeState(new SearchState());
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
