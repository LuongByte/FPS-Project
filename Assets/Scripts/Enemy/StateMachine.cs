using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private bool initial;
    private bool changing;
    private EnemyController controller;
    //public string currentState;
    public BaseState activeState;
    //Patrol State

    // Update is called once per frame
    void Update()
    {
        if(activeState != null && changing != true){
            activeState.Perform();
            //currentState = activeState.ToString();
        }
    }

    //Sets up default state
    public void Initialise()
    {
        initial = true;
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
        //Wait a second before changing states
        if(initial == false)
            StartCoroutine(Changing(newState));
        //Initial Entry
        else{
            activeState = newState;
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
            initial = false;
        }
    }


    IEnumerator Changing(BaseState newState)
    {
        changing = true;
        yield return new WaitForSeconds(controller.reactionTime);
        changing = false;
        activeState = newState;
        if(activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
        
    }

}
