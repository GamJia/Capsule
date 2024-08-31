using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private CapsuleArray[] capsuleArray; 

    private Dictionary<CapsuleID, CapsuleData> capsuleDictionary = new Dictionary<CapsuleID, CapsuleData>();

    void OnEnable()
    {
        GenerateDictionary();
    }

    private void GenerateDictionary()
    {
        capsuleDictionary.Clear(); 

        for (int i = 0; i < capsuleArray.Length; i++)
        {
            CapsuleData data = new CapsuleData
            {
                capsule = capsuleArray[i].capsuleData.capsule,
                capsuleScore = capsuleArray[i].capsuleData.capsuleScore
            };
            capsuleDictionary.Add(capsuleArray[i].capsuleID, data);
        }
    }

    public CapsuleData? GetCapsuleData(CapsuleID id)
    {
        if (capsuleDictionary.TryGetValue(id, out CapsuleData data))
        {
            return data;
        }
        return null;
    }

    public GameObject GetCapsule(CapsuleID id)
    {
        if (capsuleDictionary.TryGetValue(id, out CapsuleData data))
        {
            return data.capsule;
        }
        return null;
    }

    public GameObject GetNextCapsule(CapsuleID currentID)
    {
        int index = Array.FindIndex(capsuleArray, data => data.capsuleID == currentID);
        if (index == -1 || index + 1 >= capsuleArray.Length)
        {
            return null;
        }
        return capsuleArray[(index + 1) % capsuleArray.Length].capsuleData.capsule;
    }
}

[Serializable]
public struct CapsuleData
{
    [SerializeField] private GameObject _capsule;
    [SerializeField] private int _capsuleScore;

    public GameObject capsule
    {
        get { return _capsule; }
        set { _capsule = value; }
    }
    public int capsuleScore
    {
        get { return _capsuleScore; }
        set { _capsuleScore = value; } 
    }
}


[Serializable]
public struct CapsuleArray
{
    [SerializeField] private CapsuleID _capsuleID;
    [SerializeField] private CapsuleData _capsuleData;

    public CapsuleID capsuleID { get { return _capsuleID; } }
    public CapsuleData capsuleData { get { return _capsuleData; } }
}
