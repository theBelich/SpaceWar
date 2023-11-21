using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInit : MonoBehaviour
{
    [SerializeField] private PlanetMovement movement;
    [SerializeField] private EllipseRenderer ellipseRenderer;
    [SerializeField] private CelestialBodyInfo celestialBodyInfo;
    [SerializeField] private Transform parent;


    [SerializeField] private Planet planet;

    void Start()
    {
        Init(planet, parent);
    }

    public void Init(Planet planet, Transform parent)
    {
        this.planet = planet;
        movement.Init(planet.ellipse, planet.orbitPeriod);
        ellipseRenderer.Init(planet.ellipse);
        gameObject.SetActive(true);
        celestialBodyInfo.SetParentSystem(parent);
    }
}
