using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    private TurretAI turretAI;

    private void Awake()
    {
        turretAI = GetComponent<TurretAI>();
    }

    public void TakeDamage(int damage)
    {
        if (turretAI != null)
        {
            turretAI.TakeDamage(damage);
        }
    }
}
