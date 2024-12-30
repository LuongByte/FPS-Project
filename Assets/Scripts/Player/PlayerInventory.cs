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
        else if(color == 3)
            hasYel = true;
    }
    public bool CheckCard(float color)
    {
        if(color == 1)
            return hasRed;
        else if(color == 2)
            return hasBlu;
        else if(color == 3)
            return hasYel;
        else
            return false;
    }

    public void CollectLoot(float value)
    {
        lootCounter += value;
        motor.baseSpeed -= lootCounter * 0.07f;
    }

    public float SecureLoot()
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

    public bool Full(float val)
    {
        if(val == 0 && lootCounter == maxLoot)
            return true;
        if(lootCounter + val > maxLoot )
            return true;
        else
            return false;
    }
}
