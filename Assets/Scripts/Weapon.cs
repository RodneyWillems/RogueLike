using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool m_tryhit;

    private bool m_hit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_tryhit)
        {
            m_hit = true;
        }
    }

    public bool CheckIfHit()
    {
        if (m_hit)
        {
            m_hit = false;
            return true;
        }
        return false;
    }
}
