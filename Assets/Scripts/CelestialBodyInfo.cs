using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodyInfo : MonoBehaviour
{
    [SerializeField] private Transform _parentSystem;
    
    public void SetParentSystem(Transform parent)
    {
        _parentSystem = parent;
    }

    public Transform GetParentSystem()
    {
        return _parentSystem;
    }
}
