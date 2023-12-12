using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Claw : MonoBehaviour
{
    [SerializeField] private List<GameObject> capsuleList;
    [SerializeField] private GameObject capsule;
    [SerializeField] private Text capsuleText;
    private GameObject currentCapsule;
    private Animator clawAnimator;

    void Start()
    {
        clawAnimator=transform.GetComponent<Animator>();         
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
    }

    void Create()
    {
        int currentIndex=2-capsuleList.Count;
        for(int i=0;i<currentIndex;i++)
        {
            int randomIndex = Random.Range(0, 5);
            capsuleList.Add(capsule.transform.GetChild(randomIndex).gameObject);
        }

        clawAnimator.SetTrigger(capsuleList[0].GetComponent<Capsule>().capsuleData.CapsuleName);

        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
        currentCapsule = Instantiate(capsuleList[0], spawnPoint, Quaternion.identity);
        currentCapsule.GetComponent<Rigidbody2D>().isKinematic = true;

        capsuleText.text = capsuleList[1].GetComponent<Capsule>().capsuleData.CapsuleName;
        
    }
    
    void Drag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(currentMousePosition.x, -3.1f, 3.1f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if(currentCapsule!=null)
        {
            currentCapsule.transform.position=new Vector3(transform.position.x, transform.position.y - 1.84f, transform.position.z);
        }
        
    }

    void Drop()
    {
        //clawAnimator.SetTrigger("Dropped");
        if(currentCapsule!=null)
        {
            currentCapsule.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        capsuleList.RemoveAt(0);
        Create();
    }
}
