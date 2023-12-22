using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class Claw : MonoBehaviour
{
    [SerializeField] private CapsuleStorage capsuleStorage;
    [SerializeField] private List<GameObject> capsuleList;
    [SerializeField] private Text capsuleText;
    [SerializeField] private GameObject guide;
    [SerializeField] private GameObject currentCapsule;
    [SerializeField] private Transform capsule;
    private Animator clawAnimator;
    public bool isDragAvailable;
    
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
        isDragAvailable=true;
        clawAnimator=transform.GetComponent<Animator>();  

        Create();
        
    }
    void Update()
    {
        if(isDragAvailable)
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
                currentCapsule.SetActive(true);
                currentCapsule.transform.GetComponent<Image>().sprite = capsuleList[0].GetComponent<SpriteRenderer>().sprite;
                float spriteWidth = capsuleList[0].GetComponent<SpriteRenderer>().sprite.rect.width;
                float spriteHeight = capsuleList[0].GetComponent<SpriteRenderer>().sprite.rect.height;
                float targetScale = 0.01125f;
                currentCapsule.transform.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(spriteWidth * targetScale, spriteHeight * targetScale);

                clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);
                capsuleText.text = capsuleList[1].GetComponent<Capsule>().capsuleData.CapsuleName;
            }
        }

    }


    void Create()
    { 
        int currentIndex=2-capsuleList.Count;

        for(int i=0;i<currentIndex;i++)
        {
            capsuleList.Add(capsuleStorage.GetCapsule((CapsuleID)Random.Range(0, 5)));
        }
        
        
    }

    void Drag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(currentMousePosition.x, -3.6f, 3.6f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        guide.SetActive(true);
        
    }

    void Drop()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
        GameObject currentCapsule = Instantiate(capsuleList[0], spawnPoint, Quaternion.identity,capsule);

        AudioManager.Instance.PlaySFX(AudioID.Drop);        

        guide.SetActive(false);

        capsuleList.RemoveAt(0);
        isDragAvailable=false;
        Create();
    }

    public void Merge(Capsule first, Capsule second)
    {
        int nextLevel = first.capsuleData.CapsuleLevel + 1;
        GameObject nextCapsule = capsuleStorage.GetCapsule((CapsuleID)nextLevel);
        Vector3 mergePosition = (first.transform.position + second.transform.position) / 2f;
        Instantiate(nextCapsule, mergePosition, Quaternion.identity,capsule);
        nextCapsule.GetComponent<Animator>().SetTrigger("isMerged");
        
        AudioManager.Instance.PlaySFX(AudioID.Merge);

        MMVibrationManager.Vibrate();

        Destroy(first.gameObject);
        Destroy(second.gameObject);
    }


    
}
