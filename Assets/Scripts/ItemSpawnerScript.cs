using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
{
    [SerializeField] SpawnItem m_spawnItems;
    [SerializeField] Sprite[] m_spawnItemSprites;
    [SerializeField] float m_xSpawnClamp;
    [SerializeField] float m_ySpawnMin;
    [SerializeField] float m_ySpawnMax;
    [Header("Time")]
    [SerializeField] float m_spawnStartTime;
    [SerializeField] float m_minWaitTime;
    [SerializeField] float m_maxWaitTime;

    private void Start()
    {
        StartSpawning(m_spawnStartTime);
    }
    void StartSpawning(float m_startTime)
    {
        Invoke("SpawnRandomItem", m_startTime);
    }

    public void Spawn()
    {
        Invoke("SpawnRandomItem",Random.Range(m_minWaitTime,m_maxWaitTime));
    }
    void SpawnRandomItem()
    {
        int i = 0;
        //int i = Random.Range(0, 4);
        //if (i > 0)
        //    i = 1;
        Vector3 m_spawnPosition = new Vector3(Random.Range(-m_xSpawnClamp, m_xSpawnClamp), Random.Range(m_ySpawnMin, m_ySpawnMax), 1);
        var itemSpawned = Instantiate(m_spawnItems, m_spawnPosition, Quaternion.identity);
        itemSpawned.SetupSpawnItem((SpawnType)i, m_spawnItemSprites[i]);
    }
}
