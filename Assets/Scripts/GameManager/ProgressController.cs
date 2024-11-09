using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ProgressController : MonoBehaviour
{
    private float lootSecured;
    [SerializeField]
    private TextMeshProUGUI promptMess;

    public void UpdateProgress(GameObject trigger, string message)
    {
        promptMess.text = message;
        Destroy(trigger);
    }

    public void UpdateLoot(float value)
    {
        lootSecured += value;
    }

    public float GetLoot()
    {
        return lootSecured;
    }
    
}
