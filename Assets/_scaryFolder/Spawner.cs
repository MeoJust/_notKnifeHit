using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnerSO _currentSpawn;

    void Start()
    {
        SpawnDaPrefab();
    }

    void SpawnDaPrefab()
    {
        foreach (Transform point in _currentSpawn.GetSpawnPoints())
        {
            if (_currentSpawn.GetSpawnChance() == 1)
            {
                Instantiate(
                    _currentSpawn.GetPrefabToSpawn(), 
                    new Vector2(point.transform.position.x, point.transform.position.y + 2.5f),
                    Quaternion.identity,
                    FindObjectOfType<Target>().TargetSolid().transform);
            }
        }
    }
}
