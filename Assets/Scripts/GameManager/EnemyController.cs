using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool killCheck, detectCheck, progressCheck;
    private bool undetected;
    private int rand;
    private float spawnDelay, spawnTimer;
    private float progressCount, killCount;
    private GameObject player;
    private PlayerShoot playerShoot;
    private Transform spawnpoint;
    private float alertLevel;
    [SerializeField]
    private GameObject enemies;
    [SerializeField]
    private Transform spawnpoint1;
    [SerializeField]
    private Transform spawnpoint2;
    public float baseSpeed;
    public float reactionTime;
    public float sightDistance;
    // Start is called before the first frame update
    void Start()
    {
        alertLevel = 0;
        killCount = 0;
        progressCount = 0;
        undetected = true;
        killCheck = false;
        detectCheck = false;
        progressCheck = false;
        spawnTimer = 0f;
        spawnDelay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(alertLevel < 3){
            if(!detectCheck && !undetected){
                UpdateAlert();
                detectCheck = true;
            }

            if(!killCheck && killCount > 5){
                UpdateAlert();
                killCheck = true;
            }
            
            if(!progressCheck && progressCount > 2){
                UpdateAlert();
                progressCheck = true;
            }
        }
        else{
            if(enemies.transform.childCount < 17){
                if(spawnDelay < spawnTimer){
                    GameObject newEnemy;
                    if(spawnpoint2 != null){
                        rand = Random.Range(1, 3);
                        if(rand == 1)
                            spawnpoint = spawnpoint1;
                        else
                            spawnpoint = spawnpoint2;
                    }
                    else
                        spawnpoint = spawnpoint1;
                    rand = Random.Range(1, 4);
                    if(rand == 3)
                        newEnemy = Instantiate(Resources.Load("Prefabs/Enemies/Heavy") as GameObject, spawnpoint.position, spawnpoint.rotation);
                    else
                        newEnemy = Instantiate(Resources.Load("Prefabs/Enemies/Medium") as GameObject, spawnpoint.position, spawnpoint.rotation);
                    newEnemy.transform.SetParent(enemies.transform);
                    Enemy enem = newEnemy.GetComponent<Enemy>();
                    Target target = newEnemy.GetComponent<Target>();
                    target.Spawned();
                    enem.controller = gameObject.GetComponent<EnemyController>();
                    spawnTimer = 0;
                }
                else
                    spawnTimer += Time.deltaTime;
            }
                
        }
    }

    public float GetAlert()
    {
        return alertLevel;
    }
    public void UpdateAlert()
    {
        alertLevel += 1;
        reactionTime -= 0.05f;
        sightDistance += 10f;
        baseSpeed += 5;
    }

    public void UpdateKill()
    {
        killCount++;
    }
    public void UpdateProgress()
    {
        progressCount++;
    }
    
    public float GetKill()
    {
        return killCount;
    }
    public void Detect(){
        undetected = false;
    }
}
