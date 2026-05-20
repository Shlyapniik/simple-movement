using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    private int chunkSize = 20;
    private int renderDistance = 2;

    public Transform player;

    private Dictionary<Vector2Int, GameObject> spawnedChunks = new();
    private Vector2Int currentPlayerChunk;

    private void Start()
    {
        GenerateChunksAroundPlayer();
    }

    private void Update()
    {
        Vector2Int newPlayerChunk = GetPlayerChunkPosition();

        if (newPlayerChunk != currentPlayerChunk)
        {
            currentPlayerChunk = newPlayerChunk;

            GenerateChunksAroundPlayer();
            RemoveFarChunks();
        }
    }

    private Vector2Int GetPlayerChunkPosition()
    {
        int x = Mathf.FloorToInt(player.position.x / chunkSize);
        int z = Mathf.FloorToInt(player.position.z / chunkSize);

        return new Vector2Int(x, z);
    }

    private void GenerateChunksAroundPlayer()
    {
        Vector2Int playerChunk = GetPlayerChunkPosition();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int chunkPosition = new Vector2Int(
                    playerChunk.x + x,
                    playerChunk.y + z
                );

                if (!spawnedChunks.ContainsKey(chunkPosition))
                {
                    SpawnChunk(chunkPosition);
                }
            }
        }
    }

    private void SpawnChunk(Vector2Int chunkCrood)
    {
        Vector3 worldPosition = new Vector3(
            chunkCrood.x * chunkSize,
            0f,
            chunkCrood.y * chunkSize
        );

        GameObject randomChunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        GameObject newChunk = Instantiate(
            randomChunk,
            worldPosition,
            Quaternion.identity
        );

        spawnedChunks.Add(chunkCrood, newChunk);
    }

    private void RemoveFarChunks()
    {
        Vector2Int playerChunk = GetPlayerChunkPosition();

        List<Vector2Int> chunksToRemove = new();

        foreach (var chunk in spawnedChunks)
        {
            float distanceX = Mathf.Abs(chunk.Key.x - playerChunk.x);
            float distanceZ = Mathf.Abs(chunk.Key.y - playerChunk.y);

            if (distanceX > renderDistance || distanceZ > renderDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkCrood in chunksToRemove)
        {
            Destroy(spawnedChunks[chunkCrood]);
            spawnedChunks.Remove(chunkCrood);
        }
    }
}