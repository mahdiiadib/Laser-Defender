using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;

    static ScoreKeeper instance;

    void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int Score => score;

    public void ModifyScore(int x)
    {
        score = Mathf.Clamp(score + x, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
