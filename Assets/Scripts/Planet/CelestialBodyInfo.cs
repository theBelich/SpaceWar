using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodyInfo : MonoBehaviour
{
    [SerializeField] private Transform _parentSystem;
    
    public Transform GetParentSystem()
    {
        return _parentSystem;
    }
}
