using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;
public class Claw : MonoBehaviour
{
    public CapsuleStorage capsuleStorage;
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isDragging = false;
    private Vector2 dragStartPos;
    private GameObject capsule;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private Transform capsuleGroup;
    [SerializeField] private List<GameObject> capsuleList;



    private const float MinX = -350f;
    private const float MaxX = 350f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        CreateCapsule();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDrag();
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                OnDrag();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
    }

    private void BeginDrag()
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint))
        {
            dragStartPos = localPoint;
            if (dragStartPos.y < 0)
            {
                isDragging = true;
            }
        }
    }

    private void OnDrag()
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint))
        {
            Vector3 newPosition = rectTransform.localPosition + (Vector3)(localPoint - dragStartPos);
            newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
            rectTransform.localPosition = new Vector3(newPosition.x,762,0);
        }
    }

    private void EndDrag()
    {
        isDragging = false;
        if (capsule != null)
        {
            Rigidbody2D capsuleRigidbody = capsule.GetComponent<Rigidbody2D>();
            if (capsuleRigidbody != null)
            {
                capsuleRigidbody.bodyType = RigidbodyType2D.Dynamic;
                capsuleRigidbody.AddForce(new Vector2(0, -2000), ForceMode2D.Impulse);
            }

            capsule.transform.SetParent(capsuleGroup);     

            capsule=null;       
            Invoke("UpdateCapsule", 1f);
        }
    }

    private void UpdateCapsule()
    {        
        capsuleList.RemoveAt(0);
        CreateCapsule();
    }


    private void CreateCapsule()
    {
        int currentIndex=2-capsuleList.Count;

        for(int i=0;i<currentIndex;i++)
        {
            capsuleList.Add(capsuleStorage.GetCapsule((CapsuleID)Random.Range(0, 5)));
        }

        if (capsule==null)
        {
            capsule = Instantiate(capsuleList[0], rectTransform);
            RectTransform capsuleRectTransform = capsule.GetComponent<RectTransform>();
            capsuleRectTransform.localPosition = new Vector3(0, -180, 0);

            float rotationValue = (180 - capsuleRectTransform.rect.width) / 6;

            left.transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
            right.transform.localRotation = Quaternion.Euler(0, 0, -rotationValue);

        }
        
    }


}
