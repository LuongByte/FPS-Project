using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using System;

public class PlayerUI : MonoBehaviour
{
    private bool onScreen;
    private float time;
    private float warningTimer, updateTimer;
    private int min, sec;
    [SerializeField]private TextMeshProUGUI clock, dialogue, objective;
    [SerializeField]private Animator exclaimPoint;
    // Start is called before the first frame update
    void Start()
    {  
        time = 0;
        warningTimer = 0;
        updateTimer = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        min = Mathf.FloorToInt(time / 60);
        sec = Mathf.FloorToInt(time % 60);
        UpdateTime(min, sec);
        if(onScreen){
            if(warningTimer > 5f){
                dialogue.text = string.Empty;
                onScreen = false;
                warningTimer = 0;
            }
            else
                warningTimer += Time.deltaTime;
        }
        if(exclaimPoint.GetBool("Updating") == true){
            if(updateTimer > 1f){
                exclaimPoint.SetBool("Updating", false);
                updateTimer = 0;
            }
            else
                updateTimer += Time.deltaTime;
        }
        
    }

    private void UpdateTime(int x, int y)
    {
        clock.text = string.Format("{0:00}:{1:00}", x, y);
    }

    public void UpdateObjective(string hint, string message)
    {
        objective.text = hint;
        dialogue.text = message;
        onScreen = true;
        exclaimPoint.SetBool("Updating", true);
    }
}
