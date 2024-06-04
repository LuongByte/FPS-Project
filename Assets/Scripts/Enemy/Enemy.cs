using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    public NavMeshAgent Agent {get => agent; }
    public Path path;
    [Header("Sight Values")]
    public float fieldofView =  90f;
    public float sightDistance = 40f;
    public float eyePosition;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    [SerializeField]
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInView();
        currentState = stateMachine.activeState.ToString();
    }

    public bool PlayerInView()
    {
        if(player != null){
            //Check if player is within view distance 
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance){
                //Calculates and checks if player angle is within FOV
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyePosition);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldofView && angleToPlayer <= fieldofView){
                    //Check if line of sight blocked by object
                    Ray ray = new Ray(transform.position + (Vector3.up * eyePosition), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    //Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    if(Physics.Raycast(ray, out hitInfo, sightDistance)){
                        if(hitInfo.transform.gameObject == player){
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                    
                    
                }
            }
        }
        return false;
    }
}
