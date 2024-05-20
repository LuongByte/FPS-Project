public abstract class BaseState{
    //Sets up enemy properties
    public Enemy enemy;
    public StateMachine stateMachine;
    public abstract void Enter();

    //Update States constantly
    public abstract void Perform();

    //Called before changing to new state
    public abstract void Exit();

}