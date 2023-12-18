using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Claw : MonoBehaviour
{
    [SerializeField] private CapsuleStorage capsuleStorage;
    [SerializeField] private List<GameObject> capsuleList;
    [SerializeField] private Text capsuleText;
    [SerializeField] private Image capsuleSprite;
    [SerializeField] private GameObject guide;
    private Animator clawAnimator;
    
    public static Claw Instance => instance;
    private static Claw instance;

    private void Awake() 
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        clawAnimator=transform.GetComponent<Animator>();  
        //Init();
        Create();
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }

        if(capsuleList.Count>1)
        {
            capsuleSprite.sprite = capsuleList[0].GetComponent<SpriteRenderer>().sprite;
            float spriteWidth = capsuleList[0].GetComponent<SpriteRenderer>().sprite.rect.width;
            float spriteHeight = capsuleList[0].GetComponent<SpriteRenderer>().sprite.rect.height;
            float targetScale = 0.01f;
            capsuleSprite.rectTransform.sizeDelta = new Vector2(spriteWidth * targetScale, spriteHeight * targetScale);

            clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);
            capsuleText.text = capsuleList[1].GetComponent<Capsule>().capsuleData.CapsuleName;
        }
    }



    // void Init()
    // {
    //     int randomIndex = Random.Range(0, 5);
    //     capsuleList.Add(capsuleStorage.GetCapsule((CapsuleID)Random.Range(0, 5)));

    //     clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);

    //     Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
    //     currentCapsule = Instantiate(capsuleList[0], spawnPoint, Quaternion.identity);
    //     currentCapsule.GetComponent<Rigidbody2D>().isKinematic = true;
    // }

    void Create()
    { 
        int currentIndex=2-capsuleList.Count;

        for(int i=0;i<currentIndex;i++)
        {
            capsuleList.Add(capsuleStorage.GetCapsule((CapsuleID)Random.Range(0, 5)));
        }
        
        
    }

    // public void Generate()
    // {
    //     clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);

    //     Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
    //     currentCapsule = Instantiate(capsuleList[0], spawnPoint, Quaternion.identity);
    //     currentCapsule.GetComponent<Rigidbody2D>().isKinematic = true;
    // }
    
    void Drag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(currentMousePosition.x, -3.5f, 3.5f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        guide.SetActive(true);
        
    }

    void Drop()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
        GameObject currentCapsule = Instantiate(capsuleList[0], spawnPoint, Quaternion.identity);

        AudioManager.Instance.PlaySFX(AudioID.Drop);        

        guide.SetActive(false);

        capsuleList.RemoveAt(0);
        Create();
    }

    public void Merge(Capsule first, Capsule second)
    {
        int nextLevel = first.capsuleData.CapsuleLevel + 1;
        GameObject nextCapsulePrefab = capsuleStorage.GetCapsule((CapsuleID)nextLevel);
        Vector3 mergePosition = (first.transform.position + second.transform.position) / 2f;
        Instantiate(nextCapsulePrefab, mergePosition, Quaternion.identity);
        
        AudioManager.Instance.PlaySFX(AudioID.Merge);

        Destroy(first.gameObject);
        Destroy(second.gameObject);
    }


    
}
