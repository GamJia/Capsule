using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    private int score=0;
    [SerializeField] Text capsuleText;
    [SerializeField] Text scoreText;


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCapsuleText(int capsuleID)
    {
        switch(capsuleID)
        {
            case 0:
                capsuleText.text = "Red";
                break;
            case 1:
                capsuleText.text = "Orange";
                break;
            case 2:
                capsuleText.text = "Yellow";
                break;
            case 3:
                capsuleText.text = "Green";
                break;
            case 4:
                capsuleText.text = "Blue";
                break;
            default:
                capsuleText.text = "Unknown";
                break;
        }
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString("D3"); 
    }



}
