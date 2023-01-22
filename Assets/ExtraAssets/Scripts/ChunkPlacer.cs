using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform _player; // Player
    [SerializeField] private Chunk[] _chunksPrefabs;
    [SerializeField] private Chunk _firstChunk;

    private List<Chunk> _spawnedChunks = new();

    void Start()
    {
        _spawnedChunks.Add(_firstChunk);
    }

    void Update()
    {
        if (_player.position.z > _spawnedChunks[^1].End.position.z - 40)
            SpawnChunk();
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(GetRandomChunk());

        //We subtract the local position of the beginning of the new chunk from the End position of the last filled chunk
        newChunk.transform.position = _spawnedChunks[^1].End.position - newChunk.Begin.localPosition;
        _spawnedChunks.Add(newChunk);

        if (_spawnedChunks.Count > 3)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private Chunk GetRandomChunk()
    {
        List<float> chances = new();
        for (int i = 0; i < _chunksPrefabs.Length; i++)
            chances.Add(_chunksPrefabs[i].ChanceFromDistance.Evaluate(_player.transform.position.z));

        float value = Random.Range(0, chances.Sum());
        float sum = 0;
        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum) return _chunksPrefabs[i];
        }
        return _chunksPrefabs[^1];
    }
}