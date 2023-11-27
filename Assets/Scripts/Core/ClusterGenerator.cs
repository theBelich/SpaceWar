using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterGenerator
{
    private float distCoeff = 400;

    public Dictionary<Vector2, Vector3> GenerateZone(Vector2 zoneSize)
    {
        var positionList = new Dictionary<Vector2, Vector3>();
        for (int i = -(int)zoneSize.x; i < (int)zoneSize.x; i++)
        {
            for (int j = -(int)zoneSize.y; j < (int)zoneSize.y; j++)
            {
                System.Random random = new System.Random();
                var randX = random.Next(-20, 20);
                var randY = random.Next(-20, 20);
                var pos = new Vector3(i * distCoeff + randX, 0, j * distCoeff + randY);
                positionList.Add(new Vector2(i,j),pos);
            }
        }


        return positionList;
    }
}
