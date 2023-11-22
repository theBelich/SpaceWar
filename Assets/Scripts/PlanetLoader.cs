using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlanetLoader : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _halfExtends;

    private bool planetsLoaded = false;
    private bool planetsUnloaded = true;

    private StarSystemInfo _planetSystemInfo;

    void Update()
    {
        SetTarget();   
    }


    private void SetTarget()
    {
        if (_camera.position.y < 180)
        {
            if (planetsLoaded)
            {
                return;
            }

            if (Physics.BoxCast(_camera.position, _halfExtends, Vector3.down))
            {
                var hitInfos = Physics.BoxCastAll(_camera.position, _halfExtends, Vector3.down);

                if (hitInfos.Length > 1)
                {
                    return;
                }
                LoadPlanetSystem(hitInfos[0].transform);
                planetsLoaded = true;
                planetsUnloaded = false;
            }
        }
        else
        {
            if (planetsUnloaded) 
            {
                return;
            }
            UnloadPlanets();
            planetsUnloaded = true;
            planetsLoaded = false;
        }

    }

    private void LoadPlanetSystem(Transform target)
    {
        _planetSystemInfo = target.GetComponent<StarSystemInfo>();
        _planetSystemInfo.InitPlanets(target);
    }

    private void UnloadPlanets()
    {
        _planetSystemInfo.TurnOffPlanets();
    }
}
