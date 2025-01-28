using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Abilities
{
    heal,
    lightning,
    speedBoost,
}

public class InventoryManager : MonoBehaviour
{
    public int Coins;
    public int Keys;
    public int Mana;

    [SerializeField] private List<Item> m_items;

    private List<Abilities> m_unlockedAbilities;

    // Start is called before the first frame update
    void Start()
    {
        m_unlockedAbilities = new()
        {
            Abilities.heal,
            Abilities.lightning,
            Abilities.speedBoost,
        };
    }

    public bool ReturnAbility(int test)
    {
        if (m_unlockedAbilities.Contains<Abilities>((Abilities)test))
        {
            return true;
        }
        return false;
    }

    public bool AddAbility(Abilities ability)
    {
        if (!m_unlockedAbilities.Contains(ability))
        {
            m_unlockedAbilities.Add(ability);
            return true;
        }
        return false;
    }

    public int ReturnAbilitySize()
    {
        return m_unlockedAbilities.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
