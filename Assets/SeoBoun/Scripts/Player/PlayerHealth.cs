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
        int targetHp = playerStat.CurHp + 35 < playerStat.MaxHp? playerStat.CurHp + 35 : playerStat.MaxHp;
        
        StartCoroutine(HealRoutine(playerStat.CurHp, targetHp));

        return false;
    }

    private void Die()
    {
        Debug.Log("Player Die");
    }

    IEnumerator HealRoutine(int curHp, int targetHp)
    {
        while (true)
        {
            playerStat.CurHp += 1;

            if (playerStat.CurHp == targetHp)
                break;

            yield return new WaitForSeconds(0.03f);
        }
    }
}

