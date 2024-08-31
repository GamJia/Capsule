using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleManager : MonoBehaviour
{
    public static CapsuleManager Instance => instance;
    private static CapsuleManager instance;
    public CapsuleStorage capsuleStorage;
    
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

    public void Merge(Capsule firstCapsule, Capsule secondCapsule)
    {
        CapsuleID firstID = firstCapsule.capsuleID;

        CapsuleData? nextCapsuleData = capsuleStorage.GetCapsuleData((CapsuleID)((int)firstID + 1));
        if (nextCapsuleData.HasValue)
        {
            GameObject nextCapsulePrefab = nextCapsuleData.Value.capsule;
            int nextCapsuleScore = nextCapsuleData.Value.capsuleScore;
            UIManager.Instance.UpdateScore(nextCapsuleScore);

            Vector3 mergePosition = (firstCapsule.transform.position + secondCapsule.transform.position) / 2f;
            GameObject nextCapsule = Instantiate(nextCapsulePrefab, mergePosition, Quaternion.identity, this.gameObject.transform);

            nextCapsule.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            //AudioManager.Instance.PlaySFX(AudioID.Merge);
            //MMVibrationManager.Vibrate();

            Destroy(firstCapsule.gameObject);
            Destroy(secondCapsule.gameObject);
        }
        else
        {
            Debug.LogError("Next capsule data not found.");
        }
    }
}
