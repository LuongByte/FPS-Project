using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{   
    private float shotTimer, switchTimer, moveTimer;
    private float damage, range, fireRate;
    public override void Enter()
    {
        damage = enemy.GetDamage();
        fireRate = enemy.GetFireRate();
        range = enemy.GetRange();
        enemy.ChangeSpeed(enemy.GetSpeed() - 10);
    }

    public override void Perform()
    {
        //If player is in view
        if(enemy.PlayerInView()){
            //Lock switch state timer and increment move/shoot timers
            switchTimer = 0;
            moveTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //Slightly moves enemies around randomly while in combat
            if(moveTimer > Random.Range(1,3)){
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        
            if(Time.time >= shotTimer){
                shotTimer = Time.time + 1f/(0.75f * fireRate);
                Shoot();
            }
            enemy.LastSeen = enemy.Player.transform.position;
        }
        //Player not in view
        else{
            switchTimer += Time.deltaTime;
            if(switchTimer > 0.5f){
                stateMachine.ChangeState(new SearchState());
            }
        }
    }


    public void Shoot()
    {
        float xSpread = Random.Range(-1.5f, 1.5f);
        float ySpread = Random.Range(-1.5f, 1.5f);
        Vector3 targetDirection = enemy.Player.transform.position - enemy.transform.position - (Vector3.up * enemy.eyePosition) + new Vector3(xSpread, ySpread, 0);
        Ray ray = new Ray(enemy.transform.position + (Vector3.up * enemy.eyePosition), targetDirection);
        RaycastHit hit;
        for(int i = 0; i < enemy.shotCount; i++){
            if(Physics.Raycast(ray, out hit, range)){
                if(hit.transform.gameObject == enemy.Player){
                        hit.transform.GetComponent<PlayerHealth>().takeDamage(damage);
                        if(hit.rigidbody != null){
                            hit.rigidbody.AddForce(-hit.normal * 100);
                        }
                    }
                else{
                    if(hit.transform.parent != null){
                            if(hit.transform.parent.CompareTag("Wall")){
                                GameObject bulletHole = GameObject.Instantiate(Resources.Load("Prefabs/BulletHole") as GameObject, hit.point, Quaternion.LookRotation(hit.normal));
                                bulletHole.transform.position += bulletHole.transform.forward/1000;
                                GameObject.Destroy(bulletHole, 20);
                            }
                        }
                }
            }
        }
        GameObject flash = GameObject.Instantiate(Resources.Load("Prefabs/MuzzleFlash") as GameObject, enemy.muzzleFlash.position, enemy.muzzleFlash.rotation);
        flash.transform.SetParent(enemy.muzzleFlash);
        GameObject.Destroy(flash, 0.075f);
        enemy.controller.Detect();
    }
    /*
    public void ProjShoot()
    {
        Transform gunbarrel = enemy.gunBarrel;
        //Create new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //Calculate direction of player
        Vector3 fireDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //Calculate and add force to bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-2f,2f),Vector3.up) * fireDirection * 90;
    }
    */
    public override void Exit()
    {
        
    }
}
