using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using System;

public class PlayerUI : MonoBehaviour
{
    private bool warningCheck;
    private float time;
    private float warningTimer;
    private int min, sec;
    [SerializeField]private TextMeshProUGUI clock;
    [SerializeField]private TextMeshProUGUI warning;
    [SerializeField]private TextMeshProUGUI objective;
    // Start is called before the first frame update
    void Start()
    {  
        time = 0;
        warningTimer = 0;
        warning.text = string.Empty;
        warningCheck = false;
    }

    void Update()
    {
        time += Time.deltaTime;
        min = Mathf.FloorToInt(time / 60);
        sec = Mathf.FloorToInt(time % 60);
        UpdateTime(min, sec);
        if(warningCheck){
            if(warningTimer > 3f){
                warning.text = string.Empty;
                warningCheck = false;
                warningTimer = 0;
            }
            else
                warningTimer += Time.deltaTime;
        }
    }
    // Update is called once per frame
    private void UpdateTime(int x, int y)
    {
        clock.text = string.Format("{0:00}:{1:00}", x, y);
    }

    public void ShowWarning(string message)
    {
        warning.text = message;
        warningCheck = true;
    }

    public void UpdateObjective(string message)
    {
        objective.text = message;
    }
}
