using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private PlayerShoot playerShoot;
    private float hearDistance;
    private float fireRate, range, damage;
    private AudioSource gunSound;
    private Vector3 lastSeen;
    private float baseSpeed;
    private float sightDistance;
    private float alertLevel;
    private float locateTimer;
    private bool damageTaken;
    private bool noiseMade;
    private Target target;
    public EnemyController controller;
    public NavMeshAgent Agent {get => agent; }
    public GameObject Player {get => player; }
    public Vector3 LastSeen {get => lastSeen; set => lastSeen = value; }
    public Path path;
    [Header("Sight Values")]
    public float eyePosition;
    public float fieldofView;
    public GameObject gun;
    public float shotCount;
    public Transform muzzleFlash;
    

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        target= GetComponent<Target>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerShoot = player.GetComponent<PlayerShoot>();
        fireRate = gun.GetComponent<WeaponStats>().fireRate;
        damage = gun.GetComponent<WeaponStats>().damage/1.5f;
        range = gun.GetComponent<WeaponStats>().range;
        gunSound = gun.GetComponent<WeaponStats>().gunSound;
        int DefaultLayer = LayerMask.NameToLayer("Default");
        gun.layer = DefaultLayer;
        alertLevel = controller.GetAlert();
        sightDistance = controller.sightDistance;
        baseSpeed = controller.baseSpeed;
        ChangeSpeed(controller.baseSpeed);
        LastSeen = Player.transform.position;
        hearDistance = 70f;
        damageTaken = false;
        stateMachine.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetDirection = transform.forward;
        //Ray ray = new Ray(transform.position + (Vector3.up * eyePosition), targetDirection);
        //Debug.DrawRay(ray.origin, ray.direction * sightDistance);
        if(controller.GetAlert() != alertLevel){
            baseSpeed = controller.baseSpeed;
            ChangeSpeed(controller.baseSpeed);
            sightDistance = controller.sightDistance;
            alertLevel = controller.GetAlert();
        }
        
        if(alertLevel >= 3){
            if(locateTimer > 2){
                LastSeen = player.transform.position;
                locateTimer = 0;
            }
            else
                locateTimer += Time.deltaTime;
        }
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

    public void ChangeSpeed(float speed)
    {
        agent.speed = speed;
    }
    public float GetSpeed()
    {
        return baseSpeed;
    }
    public bool ShotHeard()
    {
        if(playerShoot.makeNoise()){
            if(Vector3.Distance(transform.position, player.transform.position) < hearDistance){
                controller.Detect();
                return true;
            }
        }
        return false;
    }
    public void CheckDamage()
    {
        damageTaken = true;
    }
    public void ResetDamage()
    {
        damageTaken = false;
    }
    public bool DamageFelt()
    {
        return damageTaken;
    }
    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetRange()
    {
        return range;
    }
    
    public AudioSource GetSound(){
        return gunSound;
    }
}