using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarPositions : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject starPrefab;
    
    [SerializeField] private List<GameObject> closeToCameraStars;
    [SerializeField] private List<GameObject> borderStars;

    [SerializeField] private Vector2 sectorSize;
    [SerializeField] private Vector2 zoneSize;

    private Dictionary<Vector2,Vector3> currentZoneStarPositionsDictionary;

    private Dictionary<Vector2, GameObject> starPool = new Dictionary<Vector2, GameObject>();

    private Vector2 currentIndexOfCamera;
    private Vector2 oldIndexOfCamera;

    private float distCoeff = 400;
    private ClusterGenerator clusterGenerator = new ClusterGenerator();

    private void Awake()
    {
        GenerateCurrentZoneStarPositions();
        CreateSector();
        FindPositionInCluster(cameraPosition.position);
    }

    private void Update()
    {
        //UpdateCloseToCameraStars();
        //FindCloseToCameraStarPositions();
        UpdateCameraPositionInCluster();
        
    }

    private void GenerateCurrentZoneStarPositions()
    {
        currentZoneStarPositionsDictionary = clusterGenerator.GenerateZone(sectorSize);
        //closeToCameraStarPositions = currentZoneStarPositionsDictionary.Select(x => x.Value).ToList();
    }

    private GameObject SpawnStar(Vector3 spawnPostion, int i, int j)
    {
        var star = Instantiate(starPrefab, spawnPostion, Quaternion.identity);
        star.name = "start: " + i + " " + j;
        return star;
    }

    private void CreateSector()
    {
        for (int i = 0; i < zoneSize.x; i++)
        {
            for (int j = 0; j < zoneSize.y; j++)
            {
                starPool.Add( new Vector2(i,j),SpawnStar(currentZoneStarPositionsDictionary[new Vector2(i -zoneSize.x/2, j - zoneSize.y/2)], i,j));
            }
        }
    }

    private void LoadZone()
    {
        var diff = currentIndexOfCamera - oldIndexOfCamera;
        
        for (int i = 0; i < zoneSize.y; i++)
        {
            if (diff.x > 0)
            {
                var oldKey = new Vector2(currentIndexOfCamera.x - 1, i);
                var newKey = new Vector2(currentIndexOfCamera.x + zoneSize.x - 1, i);
                var star = starPool[oldKey];
                star.name = newKey + "";
                star.transform.position = new Vector3(newKey.x / 2 * distCoeff, 0, star.transform.position.z);
                starPool.Remove(oldKey);
                starPool.Add(newKey, star);
                
            }
            if (diff.x < 0)
            {
                var oldKey = new Vector2(currentIndexOfCamera.x + zoneSize.x, i);
                var newKey = new Vector2(currentIndexOfCamera.x, i);
                var star = starPool[oldKey];
                star.name = currentIndexOfCamera + "";
                star.transform.position = new Vector3(-zoneSize.x + newKey.x * distCoeff, 0, star.transform.position.z);
                starPool.Remove(oldKey);
                starPool.Add(newKey, star);
            }
        }




        for (int i = 0; i < zoneSize.y; i++)
        {
            if (diff.y > 0)
            {
                var oldKey = new Vector2(currentIndexOfCamera.y - 1, i);
                var newKey = new Vector2(currentIndexOfCamera.y + zoneSize.y, i);
                var star = starPool[oldKey];
                star.name = currentIndexOfCamera + "";
                star.transform.position = new Vector3(star.transform.position.z, 0, newKey.x / 2 * distCoeff);
                starPool.Remove(oldKey);
                starPool.Add(newKey, star);

            }
            if (diff.y < 0)
            {
                var oldKey = new Vector2(i, currentIndexOfCamera.y + zoneSize.y);
                var newKey = new Vector2(i, currentIndexOfCamera.y);
                var star = starPool[oldKey];
                star.name = currentIndexOfCamera + "";
                star.transform.position = new Vector3(star.transform.position.x, 0, newKey.y / 2 * distCoeff);
                starPool.Remove(oldKey);
                starPool.Add(newKey, star);
            }
        }
    }

    private void UpdateCameraPositionInCluster()
    {
        
        FindPositionInCluster(cameraPosition.position);
        //LoadZone();
    }

    private void FindPositionInCluster(Vector3 position)
    {
        var positionInCluster = new Vector2((int)(position.x / distCoeff), (int)(position.z / distCoeff));

        if (positionInCluster != currentIndexOfCamera)
        {
            oldIndexOfCamera = currentIndexOfCamera;
            currentIndexOfCamera = positionInCluster;
            LoadZone();
        }

    }
}
