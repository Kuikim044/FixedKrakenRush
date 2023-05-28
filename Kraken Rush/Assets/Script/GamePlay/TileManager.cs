using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab;

    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTile = new List<GameObject>();
    public Transform playerTrans;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTrans.position.z - 35 > zSpawn -(numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
            DeleteTile();

        }
    }

    private void DeleteTile()
    {
        Destroy(activeTile[0]);
        activeTile.RemoveAt(0);
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefab[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTile.Add(go);
        zSpawn += tileLength;
    }
}
