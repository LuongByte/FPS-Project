using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ProgressController : MonoBehaviour
{   
    //Holds objectives for stealth and loud
    private Dictionary<int, Dictionary<int, string[]>> objectives = new Dictionary<int, Dictionary<int, string[]>>{
        {0, new Dictionary<int, string[]>
            {
                {0, new string[] {"Get inside the compound", "There should be a keycard lying around somewhere or on a guard"}},
                {1, new string[] {"Find a way inside the warehouse", "Check upstairs there should be a card on the Manager's desk"}},
                {2, new string[] {"Get to the vault", "Something tells me the big guy has what we need to get to the vault"}},
                {3, new string[] {"Look for the code", ""}},
                {4, new string[] {"Open the vault", "Now get that puppy open"}},
                {5, new string[] {"Secure the loot", "Load the loot onto the back of the truck"}},
                {6, new string[] {"Escape Blocked, Open Gate", "Looks like they caught on to us and closed the gate. Get it open"}},
                {7, new string[] {"Escape or get More Loot", "We got enough. Your call when to leave."}},
                {8, new string[] {"", "Looks like we got our paycheck too"}}
            }

        },
        {1, new Dictionary<int, string[]>
            {
                {0, new string[] {"Get inside the compound", "There should be a keycard lying around somewhere or on a guard"}},
                {1, new string[] {"Find a way inside the warehouse", "Check upstairs there should be a card on the Manager's desk"}},
                {2, new string[] {"Get to the vault", "Something tells me the big guy has what we need to get to the vault"}},
                {3, new string[] {"Pick Up the explosives", "They shut down the system. We got to do this the old fashion way. Pick "}},
                {4, new string[] {"Blow the vault open", "Time to blow this sucker wide open"}},
                {5, new string[] {"Secure the loot in container", "Bastards sabotaged the getaway. Get the gold into the container up top."}},
                {6, new string[] {"Place a flare to call the Helicopter", "Call in the chopper. We're leaving in style."}},
                {7, new string[] {"Wait for the Chopper to arrive", ""}},
                {8, new string[] {"Escape or get More Loot", "Stay and get more or shut the door and leave"}},
                {9, new string[] {"", "Looks like we got our paycheck too"}}
            }
        
        }
    };
    private float lootSecured;
    public int approach;
    public int progressCount;
    private int skipProg;
    private GameController gameController;
    private EnemyController enemController;
    private ApproachController approController;
    private Interactable escapeOpen;
    private bool firstLoot;
    [SerializeField]private PlayerUI playerUI;
    [SerializeField]private GameObject spawnpoint;

    void Start()
    {
        progressCount = 0;
        approach = 0;
        playerUI.UpdateObjective(objectives[approach][progressCount][0], objectives[approach][progressCount][1]);
        gameController = GetComponent<GameController>();
        enemController = GetComponent<EnemyController>();
        approController = GetComponent<ApproachController>();
        firstLoot = true;
    }
    public void UpdateProgress(int skip)
    {
        if(skip != -1 && skip != 1 && skip != 0){
            if(skip < progressCount)
                progressCount++;
            skipProg = skip;
        }
        else
            progressCount += skip;
        if(skipProg == progressCount && skip != -1)
            progressCount++;
        playerUI.UpdateObjective(objectives[approach][progressCount][0], objectives[approach][progressCount][1]);
        enemController.UpdateProgress();
    }

    public void EnableEscape()
    {
        UpdateProgress(1);
        approController.GetEscape().SetActive(true);
    }

    public void AlarmRaised(int cutOff)
    {
        if(cutOff >= progressCount){
            approach = 1;
        }
        skipProg = 0;
    }
    public void TriggerEscape()
    {
        gameController.GameComplete();
    }
    public void UpdateLoot(float value)
    {
        if(firstLoot){
            UpdateProgress(1);
            GameObject call = approController.GetEscapeCall();
            if(approach == 0){
                call.GetComponent<EscapeStealth>().CloseGate();
                spawnpoint.SetActive(false);
            }
            firstLoot = false;
        }
        lootSecured += value;
    }

    public float GetLoot()
    {
        return lootSecured;
    }
    
    public int GetProgress()
    {
        return progressCount;
    }
}
