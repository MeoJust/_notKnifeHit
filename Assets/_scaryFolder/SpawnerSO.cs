using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawners", fileName = "NewSpawner")]
public class SpawnerSO : ScriptableObject
{
    [SerializeField] GameObject _prefabToSpawn;
    [SerializeField] Transform _spawnPointsPrefab;

    int _spawnChance;

    public List<Transform> GetSpawnPoints()
    {
        List<Transform> spawnPoints = new List<Transform>();
        foreach (Transform point in _spawnPointsPrefab)
        {
            spawnPoints.Add(point);
        }
        return spawnPoints;
    }

    public GameObject GetPrefabToSpawn()
    {
        return _prefabToSpawn;
    }

    public int GetSpawnChance()
    {
        _spawnChance = Random.Range(0, 4);
        return _spawnChance;
    }
}
