using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlanetLoader : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private FadeMaterials _fadeMaterials;
    [SerializeField] private StarSystemInfo _planetSystemInfo;
    [SerializeField] private VisibleGalaxy _visibleGalaxy;
    [Header("UnityComponents")]
    [SerializeField] private Transform _camera;

    [Header("Serializable")]
    [SerializeField] private Vector3 _halfExtends;

    [Range(0f, 2f)]
    [SerializeField] private int layerIndex;

    //PlanetSystemInfo
    private bool planetsLoaded = false;
    private bool planetsUnloaded = true;
    //private bool planedFaded = false;

    //StarSystemInfo
    private bool starsLoaded = true;
    private bool starsUnloaded = false;
    /*private bool starsFaded = false;

    private bool ClusterFaded = true;*/
    private void Start()
    {
        _fadeMaterials.ChangeLayer(layerIndex);
    }

    void Update()
    {
        SetTarget();   
    }


    private void SetTarget()
    {
        SpawnPlanetDistanceCheck();
        SpawnStarDistanceCheck();
    }

    private void SpawnPlanetDistanceCheck()
    {
        if (_camera.localPosition.y < 180)
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

    /*private void FadeInPlanets()
    {
        if (_camera.localPosition.y < 180 && planedFaded)
        {
            layerIndex = 0;
            _fadeMaterials.ChangeLayer(0);
            _fadeMaterials.FadeIn();
            planedFaded = false;
        }

        if (_camera.localPosition.y >= 180 && !planedFaded)
        {
            _fadeMaterials.FadeOut();
            layerIndex = 1;
            _fadeMaterials.ChangeLayer(1);
            planedFaded = true;
        }

        if (_camera.localPosition.y >= 200 && !planedFaded)
        {
            _fadeMaterials.FadeOut();
            layerIndex = 1;
            _fadeMaterials.ChangeLayer(1);
            planedFaded = true;
        }

        if (_camera.localPosition.y > 990 && !starsFaded)
        {
            _fadeMaterials.FadeOut();
            starsFaded = true;
            
        }

        if(_camera.localPosition.y > 1000 && )
        {

            _fadeMaterials.FadeIn();
        }
    }*/

    //STARS FUNCTIONS

    private void SpawnStarDistanceCheck()
    {


        if (_camera.localPosition.y < 1000)
        {
            if (starsLoaded)
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

                _visibleGalaxy.DisableColliders();

                LoadStarSystem(hitInfos[0].transform);
                starsLoaded = true;
                starsUnloaded = false;
            }
        }
        else
        {
            if (starsUnloaded)
            {
                return;
            }


            _visibleGalaxy.EnableColliders();
            UnloadStars();
            starsUnloaded = true;
            starsLoaded = false;
        }
    }


    private void LoadStarSystem(Transform target)
    {
        
    }

    private void UnloadStars()
    {
       
    }
}
