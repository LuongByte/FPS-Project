using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    private bool paused;
    private ProgressController progController;
    [SerializeField]
    private GameObject enemies;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private GameObject gameOver, levelComplete, playerUI, pauseScreen;

    void Start()
    {
        Time.timeScale = 1;
        progController = GetComponent<ProgressController>();
        playerUI.SetActive(true);
        gameOver.SetActive(false);
        pauseScreen.SetActive(false);
        levelComplete.SetActive(false);
        Cursor.visible  = false;
        paused = false;
    }

    public void GameEnd()
    {
        playerUI.SetActive(false);
        enemies.SetActive(false);
        gameOver.SetActive(true);
        Cursor.visible  = true;
        Time.timeScale = 0;
    }

    public void GameComplete()
    {
        playerUI.SetActive(false);
        enemies.SetActive(false);
        levelComplete.SetActive(true);
        score.text = "Score: " + progController.GetLoot().ToString();
        Time.timeScale = 0;
    }

    public void Pause()
    {
        paused = !paused;
        Cursor.visible = paused;
        pauseScreen.SetActive(paused);
        if(!paused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    public void Resume()
    {
        Cursor.visible  = false;
        pauseScreen.SetActive(true);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Cursor.visible  = true;
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
