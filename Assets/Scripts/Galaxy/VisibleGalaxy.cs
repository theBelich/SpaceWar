using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleGalaxy : MonoBehaviour
{
    [SerializeField] private List<ClusterInfo> clusters;

    private void UpdateVisibleGalaxy(List<ClusterInfo> newClusters, List<ClusterInfo> oldClusters)
    {
        newClusters.ForEach(c =>
        {
            clusters.Add(c);
        });
        oldClusters.ForEach(c =>
        {
            clusters.Remove(c);
        });
    }

    public void DisableColliders()
    {
        foreach (var c in clusters)
        {
            c.DisableCollider();
            c.EnableStars();
        }
    }

    public void EnableColliders()
    {
        foreach (var c in clusters)
        {
            c.EnableCollider();
            c.DisableStars();
        }
    }

}
