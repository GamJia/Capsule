using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject handle;
    [SerializeField] private List<GameObject> capsuleList;



    private const float MinX = -450f;
    private const float MaxX = 450f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        CreateCapsule();
    }

    public void BeginDrag()
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

    public void OnDrag()
    {
        if(!isDragging)
        {
            return;
        }

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint))
        {
            Vector3 newPosition = rectTransform.localPosition + (Vector3)(localPoint - dragStartPos);
            newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
            rectTransform.localPosition = new Vector3(newPosition.x, 762, 0);
            
            float t = Mathf.InverseLerp(MinX, MaxX, newPosition.x);  // 0~1 사이로 정규화
            float zRotation = Mathf.Lerp(20f, -20f, t);              // -20 ~ 20 사이 보간
            handle.transform.localEulerAngles = new Vector3(0, 0, zRotation);
        }
    }

    public void EndDrag()
    {
        if(!isDragging)
        {
            return;
        }

        isDragging = false;
        if (capsule != null)
        {
            Rigidbody2D capsuleRigidbody = capsule.GetComponent<Rigidbody2D>();
            if (capsuleRigidbody != null)
            {
                capsuleRigidbody.bodyType = RigidbodyType2D.Dynamic;
                capsuleRigidbody.AddForce(new Vector2(0, -2000), ForceMode2D.Impulse);
            }

            capsule.transform.SetParent(CapsuleManager.Instance.gameObject.transform);

            capsule = null;
            Invoke("UpdateCapsule", 1f);
            
            AudioManager.Instance.PlaySFX(AudioID.Drop);
        }
    }

    private void UpdateCapsule()
    {        
        capsuleList.RemoveAt(0);
        CreateCapsule();
    }
 

    public void CreateCapsule(bool isReset = false)
    {
        if (isReset)
        {
            capsuleList.Clear();
        }
        int currentIndex = 2 - capsuleList.Count;

        for (int i = 0; i < currentIndex; i++)
        {
            int randomID = Random.Range(0, 5);
            CapsuleID capsuleID = (CapsuleID)randomID;            
            CapsuleData? capsuleData = capsuleStorage.GetCapsuleData(capsuleID);
            
            if (capsuleData.HasValue)
            {
                capsuleList.Add(capsuleData.Value.capsule);
                UIManager.Instance.UpdateCapsuleText((int)capsuleID);                
            }
           
        }

        if (capsule == null && capsuleList.Count > 0)
        {
            capsule = Instantiate(capsuleList[0], rectTransform);
            RectTransform capsuleRectTransform = capsule.GetComponent<RectTransform>();
            capsuleRectTransform.localPosition = new Vector3(0, -180, 0);

            float rotationValue = (180 - capsuleRectTransform.rect.width) / 6;

            left.transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
            right.transform.localRotation = Quaternion.Euler(0, 0, -rotationValue);
        }
    }

    public void DestroyCapsule()
    {
        if(capsule)
        {
            Destroy(capsule);
        }

        float rotationValue = 30;

        left.transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        right.transform.localRotation = Quaternion.Euler(0, 0, -rotationValue);
    }


}
