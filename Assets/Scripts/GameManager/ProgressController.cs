using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ProgressController : MonoBehaviour
{
    private float lootSecured;
    private GameController gameController;
    private EnemyController enemController;
    private bool firstLoot;
    [SerializeField]private PlayerUI playerUI;
    [SerializeField]private GameObject escapeZoom;
    [SerializeField]private GameObject gate;
    [SerializeField]private GameObject spawnpoint;

    void Start()
    {
        gameController = GetComponent<GameController>();
        enemController = GetComponent<EnemyController>();
        gate.GetComponent<Animator>().SetBool("IsOpen", true);
        escapeZoom.SetActive(false);
        firstLoot = true;
    }
    public void UpdateProgress(GameObject trigger, string message)
    {
        playerUI.UpdateObjective(message);
        enemController.UpdateProgress();
        Destroy(trigger);
    }

    public void EnableEscape()
    {
        playerUI.UpdateObjective("Escape or get More Loot");
        Destroy(spawnpoint);
        escapeZoom.SetActive(true);
    }

    public void TriggerEscape()
    {
        gameController.GameComplete();
    }
    public void UpdateLoot(float value)
    {
        if(firstLoot){
            playerUI.UpdateObjective("Escape Blocked, Open Gate");
            gate.GetComponent<Animator>().SetBool("IsOpen", false);
            firstLoot = false;
        }
        lootSecured += value;
    }

    public float GetLoot()
    {
        return lootSecured;
    }
    
}
