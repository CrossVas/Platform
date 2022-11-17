using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator instance;
    public Transform levelStartPoint;

    public List<LevelChunk> levelPrefabs = new List<LevelChunk>();
    public List<LevelChunk> chunks = new List<LevelChunk>();

    private const float radius = 15f;
    public Transform player;
    

    private void Awake()
    {
        instance = this;
    }
    
    void Start() {
        GenerateInitialPieces();
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, chunks[chunks.Count - 1].exitPoint.position) < radius)
        {
            AddChunk();
        }

        if (Vector3.Distance(player.position, chunks[0].startPoint.position) > radius * 2)
        {
            removeOldChunk();
        }
    }

    // Chunks manipulation

    public void GenerateInitialPieces() {
        for (int i = 0; i < 2; i++)
        {
            AddChunk();
        }
    }
    
    public void AddChunk()
    {
        int randomRange = Random.Range(0, 5);
        int rand = Random.Range(0, levelPrefabs.Count);
        LevelChunk chunk = (LevelChunk) Instantiate(levelPrefabs[rand]);
        chunk.transform.SetParent(transform, false);

        Vector3 spawnPosition = Vector3.zero;

        if (chunks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            Vector3 randomVector = new Vector3(chunks[chunks.Count - 1].exitPoint.position.x, randomRange);
            spawnPosition = randomVector;
        }

        chunk.transform.position = spawnPosition;
        chunks.Add(chunk);
    }

    public void removeOldChunk()
    {
        LevelChunk oldChunk = chunks[0];
        chunks.Remove(oldChunk);
        Destroy(oldChunk.gameObject);
    }
}
