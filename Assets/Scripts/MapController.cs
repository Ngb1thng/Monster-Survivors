using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject curentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();   
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!curentChunk)
        {
            return;
        }
        if (pm.moveDir.x >0 && pm.moveDir.y == 0) //right
        {
            if(!Physics2D.OverlapCircle(curentChunk.transform.Find("Right").position,checkerRadius, terrainMask)) 
            {
                noTerrainPosition = curentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0)  //left
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0)  //down
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) //right up
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Right up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Right up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) //right down
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Right down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Right down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0)  //left up
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Left up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Left up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0)  //left down
        {
            if (!Physics2D.OverlapCircle(curentChunk.transform.Find("Left down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = curentChunk.transform.Find("Left down").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0,terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand],noTerrainPosition,Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f) 
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        foreach(GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position); 
            if(opDist > maxOpDist) 
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
