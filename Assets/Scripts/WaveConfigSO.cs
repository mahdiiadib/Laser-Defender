using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float timeBetweenEnemySpawns = 1;
    [SerializeField] float spawnTimeVariance = 0;
    [SerializeField] float minSpawnTime = 0.2f;

    public Transform StartingWaypoint => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new();
        for (int i = 0; i < pathPrefab.childCount; i++)
        {
            waypoints.Add(pathPrefab.GetChild(i));
        }
        return waypoints;
    }

    public float MoveSpeed => moveSpeed;

    public int EnemyCount => enemyPrefabs.Count;

    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }
}
