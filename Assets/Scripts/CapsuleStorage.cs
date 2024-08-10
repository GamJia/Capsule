using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum CapsuleID
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Purple,
    Pink,
    Copper,
    Silver,
    GOLD
}

[CreateAssetMenu]
public class CapsuleStorage : ScriptableObject
{
    public static CapsuleStorage Instance => instance;
    private static CapsuleStorage instance;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        GenerateDictionary(); 
        
    }
    [SerializeField] CapsuleArray[] capsuleArray;

    Dictionary<CapsuleID, GameObject> capsuleDictionary = new Dictionary<CapsuleID, GameObject>();

    void GenerateDictionary()
    {
        for (int i = 0; i < capsuleArray.Length; i++)
        {
            capsuleDictionary.Add(capsuleArray[i].capsuleID, capsuleArray[i].capsule);
        }
    }

    public GameObject GetCapsule(CapsuleID id)
    {
        Debug.Assert(capsuleArray.Length > 0, "No Capsule!!");

        if (capsuleDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (capsuleDictionary.ContainsKey(id))
        {
            return capsuleDictionary[id];
        }
        else
        {
            return null;
        }
    }

    public GameObject GetNextCapsule(CapsuleID currentID)
    {
        for (int i = 0; i < capsuleArray.Length; i++)
        {
            if (capsuleArray[i].capsuleID == currentID)
            {
                int nextIndex = (i + 1) % capsuleArray.Length; 
                return capsuleArray[nextIndex].capsule;
            }
        }
        return null;
    }

    
}

[Serializable]
public struct CapsuleArray
{
    [SerializeField] GameObject _capsule;
    [SerializeField] CapsuleID _capsuleID;

    public GameObject capsule { get { return _capsule; } }
    public CapsuleID capsuleID { get { return _capsuleID; } }
}
