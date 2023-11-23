using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarInfo : MonoBehaviour
{
    [SerializeField] private List<Planet> planets;

    public List<Planet> GetPlanets() { return planets; }
}
