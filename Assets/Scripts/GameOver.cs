using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject currentCapsule;
    [SerializeField] private float deadTime;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        GetComponent<Collider2D>().enabled = true;
        currentCapsule=null;
        deadTime=0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other)
        {
            if (!currentCapsule)
            {
                currentCapsule = other.gameObject;
            }
            
            deadTime+=Time.deltaTime;

            if(deadTime>2)
            {
                GameManager.Instance.GameOver();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(currentCapsule)
        {
            currentCapsule=null;
            deadTime=0;
        }
    }

}
