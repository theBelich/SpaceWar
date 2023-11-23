using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInit : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlanetMovement movement;
    [SerializeField] private EllipseRenderer ellipseRenderer;
    
    [Header("UnityProperties")]
    [SerializeField] private Transform parent;
    [SerializeField] private Material material;

    [Header("Serializables")]
    [SerializeField] private float fadeCoefficient;
    [SerializeField] private Planet planet;
    

    private float fadeTime;
    
    void Start()
    {
        Init(planet, parent);
    }

    public void Init(Planet planet, Transform parent)
    {
        gameObject.SetActive(true);
        this.planet = planet;
        movement.Init(planet.ellipse, planet.orbitPeriod);
        ellipseRenderer.Init(planet.ellipse);
    }


    private void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel != 0)
        {

            fadeTime += mouseWheel * fadeCoefficient;

            FadeIn(material);
        }
    }

    void FadeIn(Material material)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(0, 1, fadeTime));

    }
}
