using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float loadDelay = 1;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        if (scoreKeeper != null) scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadScene("Game Over", loadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
