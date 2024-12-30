using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private bool changing;
    private EnemyController controller;
    //public string currentState;
    private BaseState activeState;
    public string currentState;
    //Patrol State

    // Update is called once per frame
    void Update()
    {
        if(activeState != null && changing != true){
            activeState.Perform();
            currentState = activeState.ToString();
        }
    }

    //Sets up default state
    public void Initialise()
    {
        changing = false;
        controller = GetComponent<Enemy>().controller;
        if(controller.GetAlert() >= 3)
           ChangeState(new SearchState());
        else
            ChangeState(new PatrolState());
    }
    public void ChangeState(BaseState newState)
    {
        //Check if another state is already running
        if(activeState != null){
            activeState.Exit();
        }
        activeState = newState;
        activeState.stateMachine = this;
        activeState.enemy = GetComponent<Enemy>();
        activeState.Enter();
    }
}
