using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    [SerializeField] private GameObject m_triggers;

    private List<BaseEnemy> m_aliveEnemies;

    private void Awake()
    {
        m_aliveEnemies = new();
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEntered();
        }
    }

    private void PlayerEntered()
    {
        m_triggers.SetActive(false);
        GameManager.Instance.SpawnEnemies(transform.position, this);
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        m_aliveEnemies.Add(enemy);

    }

    public void RemoveEnemy(BaseEnemy enemy)
    {
        m_aliveEnemies.Remove(enemy);
    }

}
