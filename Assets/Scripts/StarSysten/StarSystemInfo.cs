using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemInfo : MonoBehaviour
{
    [SerializeField] private List<PlanetInit> planetInfos;


    public void InitPlanets(Transform starSystem, List<Planet> planets)
    {
        for (int i = 0; i < planets.Count; i++)
        {
            planetInfos[i].gameObject.SetActive(true);
            planetInfos[i].Init(planets[i], starSystem);
        }
        transform.position = starSystem.position;
    }
}
