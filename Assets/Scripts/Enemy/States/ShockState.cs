using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockState : BaseState
{   
    private float reactTimer;
    private float rotateSpeed;
    public override void Enter()
    {
        reactTimer = 0;
        rotateSpeed = enemy.controller.reactionTime * 1000;
    }

    public override void Perform()
    {   
        enemy.Agent.SetDestination(enemy.transform.position);
        Vector3 directionToPlayer =  enemy.LastSeen - enemy.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        enemy.LastSeen = enemy.Player.transform.position;
        if(enemy.PlayerInView()){
            if(reactTimer > enemy.controller.reactionTime)
                stateMachine.ChangeState(new AttackState());
            else
                reactTimer += Time.deltaTime;
        }
        else{
            if(reactTimer > enemy.controller.reactionTime)
                stateMachine.ChangeState(new SearchState());
            else
                reactTimer += Time.deltaTime;
        }

    }

    public override void Exit()
    {
        
    }

}
