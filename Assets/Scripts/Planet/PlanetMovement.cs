using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] private CelestialBodyInfo parent;
    [SerializeField] private Transform OrbitingObject;
    private Ellipse orbitPath;

    private float orbitProgress;
    private float orbitPeriod = 3f;
    private bool orbitActive = true;

    public void Init(Ellipse ellipse, float orbitPeriod)
    {
        orbitPath = ellipse;
        this.orbitPeriod = orbitPeriod;

        if (OrbitingObject == null)
        {
            orbitActive = false;
            return;
        }

        SetOrbitingObjectPosition();
    }

    public void Update()
    {
        if (orbitActive)
        {
            AnimateOrbit();
        }
    }

    private void SetOrbitingObjectPosition()
    {
        Vector3 orbitpos = orbitPath.Evaluate(orbitProgress);
        transform.localPosition = orbitpos;
    }

    private void AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }

        float orbitspeed = 1f / orbitPeriod;
        orbitProgress += Time.deltaTime * orbitspeed;
        orbitProgress %= 1f;
        SetOrbitingObjectPosition();

    }
}
