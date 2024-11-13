using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockState : BaseState
{
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        enemy.transform.LookAt(enemy.Player.transform);
        enemy.Agent.SetDestination(enemy.transform.position);
        //PatrolCycle();
        if(enemy.PlayerInView()){
            stateMachine.ChangeState(new AttackState());
        }
        else{
            stateMachine.ChangeState(new SearchState());
        }

    }

    public override void Exit()
    {
        
    }

}
