using UnityEngine;
using PathCreation;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class CoinSpawner : MonoBehaviour
{
    public enum SpawnMode { Zone, Regular }

    [Header("Path of Level")]
    public PathCreator pathCreator;

    [Header("Object Prefab")]
    public GameObject coinPrefab;

    [Header("Finish Prefab")]
    public GameObject finishPrefab;
    [SerializeField] private int finishSpacing = 10;

    [Header("Spawn Mode")]
    public SpawnMode spawnMode = SpawnMode.Zone;

    [Header("Z Offset (Depth)")]
    [Range(-10f, 20f)]
    private float zOffset = 0f;

    [Header("Zone Settings")]
    public List<CoinCluster> coinClusters;

    [Header("Regular Settings")]
    [SerializeField] private float regularSpacing = 5f;

    [Header("Coin Value Settings")]
    [Range(1, 250)]
    [SerializeField] private int coinValue = 1;

    [Header("Limit Settings")]
    public bool Limit = false;
    public int maxCoins = 100;

    private int currentCoinCount = 0;

    void Start()
    {
        finishPrefab = Resources.Load<GameObject>("Finish");
        if (pathCreator != null && coinPrefab != null)
        {
            if (LevelEvent.Instance.Init() == EventType.BonusLevel)
            {
                int currentLevel = PlayerPrefs.GetInt("numLevelWasSelected");
                string currLvlType = Enum.GetName(typeof(LevelType), PlayerPrefs.GetInt("LvlTypeWasSelected"));
                LevelData curLvlData = LoadLevelData(currentLevel, currLvlType);
                if (curLvlData != null && curLvlData.countStar != 0)
                {
                    SpawnFinish();
                }
                else
                {
                    SpawnCoins();
                    SpawnFinish();
                }
            }
            else
            {
                SpawnCoins();
                SpawnFinish();
            }
        }
    }

    void SpawnCoins()
    {
        VertexPath path = pathCreator.path;

        if (spawnMode == SpawnMode.Zone)
        {
            SpawnCoinsInZones(path);
        }
        else if (spawnMode == SpawnMode.Regular)
        {
            SpawnCoinsRegularly(path);
        }
    }

    void SpawnCoinsInZones(VertexPath path)
    {
        float dst = 0;

        foreach (var cluster in coinClusters)
        {
            dst += cluster.startOffset;

            for (int i = 0; i < cluster.numCoins; i++)
            {
                if (Limit && currentCoinCount >= maxCoins) return;

                if (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    Quaternion rotation = path.GetRotationAtDistance(dst);
                    point.z += zOffset;

                    rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);

                    GameObject coin = Instantiate(coinPrefab, point, rotation, transform);
                    AssignCoinValue(coin);
                    currentCoinCount++;
                    dst += cluster.spacing;
                }
            }
            dst += cluster.clusterSpacing;
        }
    }

    void SpawnCoinsRegularly(VertexPath path)
    {
        float dst = 0;

        while (dst < path.length)
        {
            if (Limit && currentCoinCount >= maxCoins) return;

            Vector3 point = path.GetPointAtDistance(dst);
            Quaternion rotation = path.GetRotationAtDistance(dst);
            point.z += zOffset;

            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);

            GameObject coin = Instantiate(coinPrefab, point, rotation, transform);
            AssignCoinValue(coin);
            currentCoinCount++;
            dst += regularSpacing;
        }
    }

    void SpawnFinish()
    {
        /* if (finishPrefab != null)
         {
             VertexPath path = pathCreator.path;
             float endDistance = path.length;
             Vector3 endPoint = path.GetPointAtDistance(endDistance);
             Quaternion endRotation = path.GetRotationAtDistance(endDistance);
             endPoint.z = endDistance;
             Debug.Log(endDistance);

             endRotation = Quaternion.Euler(endRotation.eulerAngles.x, endRotation.eulerAngles.y + 90f, 0);

             Debug.Log($"End Distance: {endDistance}");
             Debug.Log($"End Point: {endPoint}");
             Debug.Log($"End Rotation: {endRotation.eulerAngles}");

             Instantiate(finishPrefab, new Vector3(endDistance-15, endPoint.y-13, 0), endRotation, transform); 
         }*/
    }

    void AssignCoinValue(GameObject coin)
    {
        Coin coinComponent = coin.GetComponent<Coin>();
        if (coinComponent != null)
        {
            coinComponent.SetCoinValue(coinValue);
        }
    }

    private LevelData LoadLevelData(int numLevel, string lvlType)
    {
        string key = lvlType + "level" + numLevel;
        object data = SaveData.Instance.GetData(key);
        if (data != null)
        {
            return JsonConvert.DeserializeObject<LevelData>(JsonConvert.SerializeObject(data));
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class CoinCluster
{
    public float startOffset;   // Distance to the start of next zone
    public int numCoins;        // Num of coins in heap
    public float spacing = 5f;  // Distance between coins in the cluster
    public float clusterSpacing; // Distance to next zone
}
