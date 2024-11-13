using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool hasRed, hasYel, hasBlu;
    private float lootCounter = 0;
    private float maxLoot = 100;
    private PlayerUI playerUI;
    private PlayerMotor motor;
    [SerializeField]
    private ProgressController controller;
    // Start is called before the first frame update
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        motor = GetComponent<PlayerMotor>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectCard(float color)
    {
        if(color == 1)
            hasRed = true;
        else if(color == 2)
            hasBlu = true;
        else
            hasYel = true;
    }
    public bool CheckCard(float color)
    {
        if(color == 1)
            return hasRed;
        else if(color == 2)
            return hasBlu;
        else
            return hasYel;
    }

    public void CollectLoot(float value, GameObject gold)
    {
        if(maxLoot >= (lootCounter + value)){
            lootCounter += value;
            motor.baseSpeed -= lootCounter * 0.07f;
            Destroy(gold);
        }
        else
            playerUI.ShowWarning("Can't Carry More!");
    }

    public float GetLoot()
    {
        float loot = lootCounter;
        motor.baseSpeed += lootCounter * 0.1f;
        lootCounter = 0;
        return loot;
    }

    public float GetMax()
    {
        return maxLoot;
    }
}
