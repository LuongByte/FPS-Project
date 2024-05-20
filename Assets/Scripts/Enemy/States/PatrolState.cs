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
        PatrolCycle();
    }

    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < 0.2f){
            waitTime += Time.deltaTime;
            if(waitTime > 3){
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
