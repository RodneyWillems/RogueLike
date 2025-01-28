using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public string StatUpgrade;
    public Sprite ItemSprite;
    public int ItemUpgrade;
    public bool StatItem;

    public void UseItem(PlayerMovement player)
    {
        if (StatItem)
        {
            player.UpgradeStat(StatUpgrade, ItemUpgrade);
        }
        else
        {
            player.RegenStat(StatUpgrade, ItemUpgrade);
        }
    }

}
