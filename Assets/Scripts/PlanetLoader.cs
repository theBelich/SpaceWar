using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLoader : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private ViewController _viewController;
    [SerializeField] private StarSystemInfo _clusterInfo;

    [SerializeField] private Transform target;
    [SerializeField] private Transform parentTarget;

    private int layer;

    private void Start()
    {
        _viewController.OnViewChanges += SetViewIndex;
    }

    private void OnDisable()
    {
        
    }

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
                
                if (hitInfo.transform.TryGetComponent<CelestialBodyInfo>(out var parentCelestialBodyInfo))
                {
                    parentTarget = parentCelestialBodyInfo.GetParentSystem();
                }
                else
                {
                    parentTarget = null;
                }

                _cameraController.SetTarget(target, parentTarget);
            }

            if (layer == 1)
            {
                LoadPlanetSystem();
            }
        }

    }

    private void LoadPlanetSystem()
    {
        var planets = target.GetComponent<ClusterInfo>().GetPlanets();
        _clusterInfo.InitPlanets(target, planets);
    }

    private void LoadCluster()
    {

    }

    private void LoadGalaxy()
    {

    }


    private void SetViewIndex(int viewIndex)
    {
        this.layer = viewIndex;
    }
}
