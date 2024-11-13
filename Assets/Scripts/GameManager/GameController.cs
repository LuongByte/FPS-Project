using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private ProgressController progController;
    [SerializeField]
    private GameObject enemies;
    [SerializeField]
    private TextMeshProUGUI score;
    public GameObject gameOver;
    public GameObject levelComplete;
    public GameObject playerUI;
    // Start is called before the first frame update


    void Start()
    {
        progController = GetComponent<ProgressController>();
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
        enemies.SetActive(false);
        gameOver.SetActive(true);
    }

    public void GameComplete()
    {
        playerUI.SetActive(false);
        enemies.SetActive(false);
        levelComplete.SetActive(true);
        score.text = "Score: " + progController.GetLoot().ToString();
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
