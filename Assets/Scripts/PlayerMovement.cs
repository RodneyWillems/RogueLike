using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Character
{
    #region Variables

    [Header("Abilities")]
    [SerializeField] private Abilities m_activeAbility;
    [SerializeField] private int m_abilityIndex;

    // Inputs
    private PlayerControls m_controls;
    private Coroutine m_attackCoroutine;
    private bool m_paused;

    // Stats
    private Statistics m_maxStats;
    private int m_ownHealth;

    // Misc
    private InventoryManager m_inventory;
    private Weapon m_weapon;
    private Rigidbody2D m_rb;

    #endregion

    #region Setup

    private void OnEnable()
    {
        m_controls.DefaultMovement.Enable();
    }

    private void OnDisable()
    {
        m_controls.DefaultMovement.Disable();
    }

    private void Awake()
    {
        m_controls = new();
        SetupControls();
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void SetupControls()
    {
        m_controls.DefaultMovement.Attack.started += PlayerAttack;
        m_controls.DefaultMovement.UseAbility.started += UseAbility;
        m_controls.DefaultMovement.CycleAbility.started += CycleAbility;
        m_controls.DefaultMovement.Pause.started += PauseGame;
        m_controls.DefaultMovement.Interact.started += Interact;
    }

    protected override void Start()
    {
        base.Start();
        m_inventory = GetComponent<InventoryManager>();
        m_abilityIndex = 0;
        SetMaxStats();
    }
    #endregion

    #region Stats
    private void SetMaxStats()
    {
        m_maxStats = new Statistics();
        m_maxStats.damage = 10;
        m_maxStats.range = 8;
        m_maxStats.speed = 8;
        m_maxStats.health = 10;
    }

    protected override void SetStats()
    {
        m_stats.damage = 1;
        m_stats.range = 4;
        m_stats.speed = 3;
        m_stats.health = 3;
    }

    public void UpgradeStat(string stat, int added = 1)
    {
        switch (stat)
        {
            case ("damage"):
                m_stats.damage += added;
                if (m_stats.damage > m_maxStats.damage)
                {
                    m_stats.damage = m_maxStats.damage;
                }
                break;
            case ("range"):
                m_stats.range += added;
                if (m_stats.range > m_maxStats.range)
                {
                    m_stats.range = m_maxStats.range;
                }
                break;
            case ("speed"):
                m_stats.speed += added;
                if (m_stats.speed > m_maxStats.speed)
                {
                    m_stats.speed = m_maxStats.speed;
                }
                break;
            case ("health"):
                m_stats.health += added;
                if (m_stats.health > m_maxStats.health)
                {
                    m_stats.health = m_maxStats.health;
                }
                break;
            default:
                break;
                
        }
    }

    public void RegenStat(string stat, int added)
    {
        switch (stat)
        {
            case "health":
                m_ownHealth += added;
                if (m_ownHealth > m_stats.health)
                {
                    m_ownHealth = m_stats.health;
                }
                break;
            case "mana":
                m_inventory.Mana += added;
                if (m_inventory.Mana > 100)
                {
                    m_inventory.Mana = 100;
                }
                break;
        }
    }

    #endregion

    #region Abilities

    public void CycleAbility(InputAction.CallbackContext _context)
    {
        // Depending on keyboard or controller the player can either cycle up or down
        if (_context.ReadValue<float>() > 0)
        {
            m_abilityIndex++;
        }
        else
        {
            if (m_abilityIndex > 0)
            {
                m_abilityIndex--;
            }
            // Cycles back from 0 to the highest
            else if (m_abilityIndex == 0)
            {
                m_abilityIndex = m_inventory.ReturnAbilitySize() - 1;
            }
        }
        // Cycles back if 
        while (m_abilityIndex > Enum.GetValues(typeof(Abilities)).Length || !m_inventory.ReturnAbility(m_abilityIndex))
        {
            m_abilityIndex++;
        }
        m_activeAbility = (Abilities)m_abilityIndex;
    }

    public void UseAbility(InputAction.CallbackContext _context)
    {
        GameManager.Instance.UsePlayerAbility(m_activeAbility);
    }

    #endregion

    #region Inputs
    public void PauseGame(InputAction.CallbackContext _context)
    {
        m_paused = !m_paused;
        if (m_paused)
        {
            Time.timeScale = 0;
            m_controls.DefaultMovement.Disable();
        }
        else
        {
            Time.timeScale = 1;
            m_controls.DefaultMovement.Enable();
        }
    }

    public void PlayerAttack(InputAction.CallbackContext _context)
    {
        if (m_attackCoroutine == null)
        {
            m_attackCoroutine = StartCoroutine(AttackCR());
        }
    }

    private IEnumerator AttackCR()
    {
        float attackTime = 0.2f;
        while (!m_weapon.CheckIfHit() && attackTime > 0)
        {
            attackTime += Time.deltaTime;
            yield return null;
        }
        m_attackCoroutine = null;
        yield return null;
    }

    private void Interact(InputAction.CallbackContext _context)
    {

    }

    #endregion

    #region Movement

    private void Move()
    {
        Vector2 _data = m_controls.DefaultMovement.Moving.ReadValue<Vector2>();
        m_rb.velocity = _data.normalized * m_stats.speed;
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    #endregion
}
