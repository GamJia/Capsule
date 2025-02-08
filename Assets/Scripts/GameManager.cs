using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;
    
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

    public void GameOver()
    {
        ClawManager.Instance.EnableTouchEvents(false);
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("isGameOver");
        }

    }

}
