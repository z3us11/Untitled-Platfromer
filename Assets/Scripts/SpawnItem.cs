using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpawnType m_spawnType;
    [SerializeField] SpriteRenderer m_spawnTypeImg;
    [SerializeField] float repairAmount;
    public float RepairAmount
    {
        get
        {
            return repairAmount;
        }
    }

    public void SetupSpawnItem(SpawnType spawnType, Sprite spawnImg)
    {
        m_spawnType = spawnType;
        m_spawnTypeImg.sprite = spawnImg;
        repairAmount = 5f;
    }
} 

public enum SpawnType
{
    Repair,
    Damage
}
