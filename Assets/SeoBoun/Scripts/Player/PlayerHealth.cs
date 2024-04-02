using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStat playerStat;

    public bool TakeHit(int damage)
    {
        playerStat.CurHp -= damage;

        if(playerStat.CurHp <= 0)
        {
            Die();
        }

        return false;
    }

    [ContextMenu("Heal")]
    public bool Heal()
    {
        playerStat.CurHp += 30;

        return false;
    }

    private void Die()
    {
        Debug.Log("Player Die");
    }
}

