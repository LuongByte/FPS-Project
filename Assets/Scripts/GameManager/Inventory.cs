using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool hasRed, hasYel, hasBlu;
    private float lootCounter = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectCard(float color)
    {
        if(color == 1)
            hasRed = true;
        else if(color == 2)
            hasYel = true;
        else
            hasBlu = true;
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

    public void CollectLoot(float value)
    {
        lootCounter += value;
    }

    public float GetLoot()
    {
        return lootCounter;
    }

}
