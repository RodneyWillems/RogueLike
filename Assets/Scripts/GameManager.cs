using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameObject[] m_potentialItemDrops;
    private GameObject[] m_enemyPrefabs;
    private GameObject[] m_roomPrefabs;
    private int m_currentFloor;
    private PlayerMovement m_player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentFloor = 0;

        if (PrefabHolder.Instance != null)
        {
            m_potentialItemDrops = PrefabHolder.Instance.PotentialItemDrops;
            m_roomPrefabs = PrefabHolder.Instance.RoomPrefabs;
            m_enemyPrefabs = PrefabHolder.Instance.EnemyPrefabs;
        }

        m_player = FindObjectOfType<PlayerMovement>();
    }

    public void UsePlayerAbility(Abilities ability)
    {
        switch (ability)
        {
            case Abilities.heal:
                
                break;
            case Abilities.lightning:
                break;
            case Abilities.speedBoost:
                break;
        }
    }

    public void ChooseItemDrop()
    {

    }

    public void SpawnEnemies(Vector2 spawnPos, Rooms currentRoom)
    {
        if (m_enemyPrefabs.Length < 1)
        {
            m_enemyPrefabs = PrefabHolder.Instance.EnemyPrefabs;
        }
        int randomEnemyType = Random.Range(0, m_enemyPrefabs.Length);
        int spawnAmount = 0;
        GameObject enemyPrefab = m_enemyPrefabs[randomEnemyType];

        switch (randomEnemyType)
        {
            case 0:
                spawnAmount = 3;
                break;
        }

        for (int i = 0; i < spawnAmount; i++)
        {
            bool emptySpace = false;
            Vector2 randomSpawn = Vector2.zero;
            int roomSize = 5;
            while (!emptySpace)
            {
                randomSpawn = Random.insideUnitCircle * roomSize + spawnPos;
                Collider[] collidersThere = Physics.OverlapSphere(randomSpawn, 1);
                if (collidersThere.Length < 1)
                {
                    emptySpace = true;
                }
            }

            BaseEnemy newEnemy = Instantiate(enemyPrefab, randomSpawn, Quaternion.identity).GetComponent<BaseEnemy>();
            currentRoom.AddEnemy(newEnemy);
        }
    }

    public void CreateFloor()
    {

    }

    public void NextFloor()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
