using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{   
    private float shootTimer, switchTimer, moveTimer;

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        //If player is in view
        if(enemy.PlayerInView()){
            //Lock switch state timer and increment move/shoot timers
            switchTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //Slightly moves enemies around randomly while in combat
            if(moveTimer > Random.Range(1,3)){
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        
            if(shootTimer > enemy.fireRate){
                Shoot();
            }
            enemy.LastSeen = enemy.Player.transform.position;
        }
        //Player not in view
        else{

            switchTimer += Time.deltaTime;
            if(switchTimer > 1){
                Debug.Log("Bob");
                stateMachine.ChangeState(new SearchState());
            }
        }
    }


    public void Shoot()
    {
        float range = 60f;
        shootTimer = 0;
        float xSpread = Random.Range(-1f, 1f);
        float ySpread = Random.Range(-1f, 1f);
        Vector3 targetDirection = enemy.Player.transform.position - enemy.transform.position - (Vector3.up * enemy.eyePosition) + new Vector3(xSpread, xSpread, 0);
        Ray ray = new Ray(enemy.transform.position + (Vector3.up * enemy.eyePosition), targetDirection);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, range)){
            if(hit.transform.gameObject == enemy.Player){
                    hit.transform.GetComponent<PlayerHealth>().takeDamage(10);
                    if(hit.rigidbody != null){
                        hit.rigidbody.AddForce(-hit.normal * 10);
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
        GameObject flash = GameObject.Instantiate(Resources.Load("Prefabs/MuzzleFlash") as GameObject, enemy.muzzleFlash.position, enemy.muzzleFlash.rotation);
        flash.transform.SetParent(enemy.muzzleFlash);
        GameObject.Destroy(flash, 0.075f);
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
