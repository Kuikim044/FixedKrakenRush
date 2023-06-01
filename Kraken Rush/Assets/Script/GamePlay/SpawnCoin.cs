using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    public float laneOffset = 2.5f;
    public float spawnInterval = 2f;
    private int coinsPerSpawn; // ?????????????????????????????????
    private float coinSpacingZ = 2f; // ?????????????????????????? Z
    public float coinSpawnOffsetZ = 3f; // ????????????????????????????? Z
    public float coinSpacingZBetweenSets = 20f;
    private float nextSpawnTime;
    private List<Transform> lanes;
    private List<float> spawnedPositions;

    private void Start()
    {
        lanes = new List<Transform>();
        for (int i = 0; i < 3; i++)
        {
            GameObject lane = new GameObject("Lane " + (i + 1));
            float laneX = CalculateLaneX(i);
            lane.transform.position = new Vector3(laneX, 0f, 0f);
            lanes.Add(lane.transform);
        }

        nextSpawnTime = Time.time;
        spawnedPositions = new List<float>();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoins();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnCoins()
    {
        int laneIndex = Random.Range(0, lanes.Count); // ????????????
        Transform lane = lanes[laneIndex];
        float randomY = Random.Range(0.5f, 3f);
        float spawnZ = spawnedPositions.Count > 0 ? spawnedPositions[spawnedPositions.Count - 1] + coinSpacingZ + coinSpacingZBetweenSets : coinSpawnOffsetZ; // ??????? Z ?????????????
        Vector3 spawnPosition = new Vector3(lane.position.x, randomY, spawnZ);
        coinsPerSpawn = Random.Range(5, 20);
        for (int i = 0; i < coinsPerSpawn; i++)
        {
            spawnedPositions.Add(spawnPosition.z);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, lane);
            spawnPosition.z += coinSpacingZ;
        }
    }


    private float CalculateLaneX(int laneIndex)
    {
        float laneX = 0f;
        if (laneIndex == 0)
        {
            laneX = -4.99f;
        }
        else if (laneIndex == 1)
        {
            laneX = 0f;
        }
        else if (laneIndex == 2)
        {
            laneX = 4.99f;
        }
        return laneX;
    }
}
