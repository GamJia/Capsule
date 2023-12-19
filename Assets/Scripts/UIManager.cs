using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    [SerializeField] private Text scoreText;
    private int score;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    
    void Start() {
        score=0;
    }

    public void CalculateScore(int plusScore)
    {
        score+=plusScore;
        if(score<100)
        {
            string scoreFormat = score.ToString("D3");
            scoreText.text = scoreFormat;
        }

        else
        {
            scoreText.text = score.ToString();
        }
    }
}
