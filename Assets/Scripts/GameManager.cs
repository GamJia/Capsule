using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;
    [SerializeField] private GameObject capsuleGroup;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject setting;

    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public void GameOver()
    {
        UIManager.Instance.UpdateScore();
        Claw.Instance.isDragAvailable=false;
        StartCoroutine(GameOverAnimation());
    }

    IEnumerator GameOverAnimation()
    {
        settingButton.SetActive(false);
        for(int i=0;i<capsuleGroup.transform.childCount;i++)
        {
            capsuleGroup.transform.GetChild(i).GetComponent<Animator>().SetTrigger("isGameOver");
            yield return new WaitForSeconds(0.1f);
        }

        setting.GetComponent<Animator>().SetTrigger("Game_Over");

        
    }
    
}
