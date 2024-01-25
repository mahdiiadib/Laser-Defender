using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int wayPointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.CurrentWave;
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[wayPointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (wayPointIndex < waypoints.Count)
        {
            Vector2 targetPos = waypoints[wayPointIndex].position;
            float delta = waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if ((Vector2)transform.position == targetPos) wayPointIndex++;
        }
        else Destroy(gameObject);
    }
}
