using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private PlayerShoot playerShoot;
    private Vector3 lastSeen;
    public NavMeshAgent Agent {get => agent; }
    public GameObject Player {get => player; }
    public Vector3 LastSeen {get => lastSeen; set => lastSeen = value; }
    public Path path;
    public Transform inventory;
    [Header("Sight Values")]
    public float fieldofView =  40f;
    public float sightDistance = 40f;
    public float eyePosition;
    public Transform muzzleFlash;
    [Header("Weapon Values")]
    [Range(0.1f, 10f)]
    public float fireRate;
    public string currentState;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        playerShoot = player.GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
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

    public bool ShotHeard()
    {
        //transform.LookAt(Player.transform);
        if(playerShoot.makeNoise()){
            return true;
            //stateMachine.ChangeState(new AttackState());
        }
        return false;
    }
    ///yield return new WaitForSeconds(1);
    ///
    IEnumerator Reaction()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
}