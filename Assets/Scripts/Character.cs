using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Statistics m_stats;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_stats = new Statistics();
        SetStats();
    }

    protected virtual void SetStats()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
