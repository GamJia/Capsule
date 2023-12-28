using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;

public class Claw : MonoBehaviour
{
    [SerializeField] private CapsuleStorage capsuleStorage;
    [SerializeField] private List<GameObject> capsuleList;
    [SerializeField] private Text capsuleText;
    [SerializeField] private GameObject guide;
    [SerializeField] private GameObject currentCapsule;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform capsuleGroup;
    [SerializeField] private GameObject optionUI;

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
        Create();

        isDragAvailable=true;
        clawAnimator=transform.GetComponent<Animator>();  
        
    }
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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

            if(!currentCapsule)
            {
                currentCapsule=Instantiate(capsuleList[0],spawnPoint.position,Quaternion.identity,capsuleGroup);
                currentCapsule.transform.GetComponent<Collider2D>().enabled=false;
                currentCapsule.transform.GetComponent<Rigidbody2D>().isKinematic=true;
                

                clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);
                capsuleText.text = capsuleList[1].GetComponent<Capsule>().capsuleData.CapsuleName;
            }

        }

        currentCapsule.SetActive(isDragAvailable);

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
        float clampedX = Mathf.Clamp(currentMousePosition.x, -4.3f,4.3f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if(currentCapsule)
        {
            currentCapsule.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        }

        guide.SetActive(true);
    
        
        
    }


    void Drop()
    {
        AudioManager.Instance.PlaySFX(AudioID.Drop);        

        guide.SetActive(false);

        currentCapsule.transform.SetParent(capsuleGroup);
        currentCapsule.transform.GetComponent<Collider2D>().enabled=true;
        currentCapsule.transform.GetComponent<Rigidbody2D>().isKinematic=false;

        currentCapsule=null;

        capsuleList.RemoveAt(0);
        isDragAvailable=false;

        Create();
        
    }

    public void Merge(Capsule first, Capsule second)
    {
        int nextLevel = first.capsuleData.CapsuleLevel + 1;
        GameObject nextCapsule = capsuleStorage.GetCapsule((CapsuleID)nextLevel);
        Vector3 mergePosition = (first.transform.position + second.transform.position) / 2f;
        Instantiate(nextCapsule, mergePosition, Quaternion.identity,capsuleGroup);
        
        AudioManager.Instance.PlaySFX(AudioID.Merge);

        MMVibrationManager.Vibrate();

        Destroy(first.gameObject);
        Destroy(second.gameObject);
    }


    
}
