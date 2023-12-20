using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;
    [SerializeField] private GameObject machineCollider;
    
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
        StartCoroutine(GameOverAnimation());
    }

    IEnumerator GameOverAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        machineCollider.SetActive(false);
    }
    
}
