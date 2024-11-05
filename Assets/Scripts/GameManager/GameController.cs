using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public GameObject gameOver;
    public GameObject levelComplete;
    public GameObject playerUI;
    // Start is called before the first frame update


    void Start()
    {
        playerUI.SetActive(true);
        gameOver.SetActive(false);
        levelComplete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameEnd()
    {
        playerUI.SetActive(false);
        gameOver.SetActive(true);
    }

    public void LevelComplete()
    {
        playerUI.SetActive(false);
        levelComplete.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
