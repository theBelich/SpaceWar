using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemInfo : MonoBehaviour
{
    [SerializeField] private List<PlanetInit> planetInfos;
    [SerializeField] private ClusterInfo defaultCluster;

    public void InitPlanets(Transform starSystem)
    {
        TurnOffPlanets();
        transform.position =  new Vector3(starSystem.position.x, 0, starSystem.position.z);
        var planets = defaultCluster.GetPlanets();

        for (int i = 0; i < planets.Count; i++)
        {
            planetInfos[i].gameObject.SetActive(true);
            planetInfos[i].Init(planets[i], starSystem);
        }
    }

    public void TurnOffPlanets()
    {
        foreach (var planet in planetInfos)
        {
            planet.gameObject.SetActive(false);
        }
    }
}
