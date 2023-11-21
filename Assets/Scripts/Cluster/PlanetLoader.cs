using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLoader : MonoBehaviour
{
    [SerializeField] private CameraZoom _zoom;

    [SerializeField] private Transform target;
    [SerializeField] private Transform parentTarget;
    
    void Update()
    {
        SetTarget();   
    }


    private void SetTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo);
            if (ray)
            {
                target = hitInfo.transform;
                parentTarget = hitInfo.transform.GetComponent<CelestialBodyInfo>().GetParentSystem();
                _zoom.SetTarget(target, parentTarget);
            }
        }
    }
}
