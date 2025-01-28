using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    public static PrefabHolder Instance;

    public GameObject[] PotentialItemDrops;
    public GameObject[] EnemyPrefabs;
    public GameObject[] RoomPrefabs;

    private void Awake()
    {
        Instance = this;
    }
}
