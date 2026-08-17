using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;
    public static WaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<WaveManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("WaveManager");
                    _instance = obj.AddComponent<WaveManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class EnemySpawnEntry
    {
        public Enemy prefab;
        public int minPerWave;
        public int maxPerWave;
    }

    [SerializeField] private List<EnemySpawnEntry> spawnEntries = new List<EnemySpawnEntry>();
    [SerializeField] private Collider spawnArea;

    private int aliveCount = 0;
    private int currentWave = 0;
    private Transform player;

    [SerializeField] private float minSpawnDistFromPlayer = 3f; // game unit

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        SpawnWave();
    }

    public void OnEnemyDied()
    {
        aliveCount--;
        if (aliveCount <= 0)
        {
            SpawnWave();
        }
    }

    public int GetCurrentWave() => currentWave;

    private void SpawnWave()
    {
        currentWave++;
        aliveCount = 0;

        foreach (EnemySpawnEntry entry in spawnEntries)
        {
            int count = Random.Range(entry.minPerWave, entry.maxPerWave + 1);
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();
                GameObject spawnedObj = ObjectPooling.Instance.InstantiateObject(
                    entry.prefab.gameObject,
                    spawnPoint,
                    Quaternion.identity
                );

                Collider col = spawnedObj.GetComponent<Collider>();
                if (col != null)
                {
                    float yOffset = col.bounds.extents.y;
                    spawnedObj.transform.position = new Vector3(spawnPoint.x, yOffset, spawnPoint.z);
                }

                aliveCount++;
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Bounds bounds = spawnArea.bounds;
        float scale = GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
        float minDist = minSpawnDistFromPlayer * scale;

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 candidate = new Vector3(x, 0f, z);

            if (!bounds.Contains(new Vector3(candidate.x, bounds.center.y, candidate.z)))
                continue;

            if (player != null && Vector3.Distance(candidate, player.position) < minDist)
                continue;

            return candidate;
        }

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 candidate = new Vector3(x, 0f, z);

            if (bounds.Contains(new Vector3(candidate.x, bounds.center.y, candidate.z)))
                return candidate;
        }

        Debug.LogWarning("WaveManager: Không tìm được vị trí spawn hợp lệ, dùng tâm sân chơi.");
        return new Vector3(bounds.center.x, 0f, bounds.center.z);
    }
}
