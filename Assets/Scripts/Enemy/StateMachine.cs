using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    //Patrol State
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null){
            activeState.Perform();
        }
    }

    //Sets up default state
    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    public void ChangeState(BaseState newState)
    {
        //Check if another state is already running
        if(activeState != null){
            activeState.Exit();
        }

        activeState = newState;

        if(activeState != null){
            //Set Up
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }

}
