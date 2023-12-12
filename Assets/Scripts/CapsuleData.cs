using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName="Capsule Data",menuName="Capsule Data")]
public class CapsuleData : ScriptableObject
{
    [SerializeField] private string capsuleName;
    [SerializeField] private int capsuleLevel;
    public string CapsuleName
    {
        get
        {
            return capsuleName;
        }
    }
    public int CapsuleLevel
    {
        get
        {
            return capsuleLevel;
        }
    }    
    

}

