using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterInfo : MonoBehaviour
{
    [SerializeField] private Collider box;
    [SerializeField] private List<Collider> _stars;
    [SerializeField] private List<Vector3> _starsCoordinates;

    public void DisableStars()
    {
        foreach (var star in _stars)
        {
            star.enabled = false;
        }
    }

    public void EnableStars()
    {
        foreach (var star in _stars)
        {
            star.enabled = true;
        }
    }

    public void DisableCollider()
    {
        box.enabled = false;
    }
    public void EnableCollider()
    {
        box.enabled = true;
    }
}
