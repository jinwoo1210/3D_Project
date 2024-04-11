using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public UnityEvent OnDie;

    public bool TakeHit(int damage)
    {
        PlayerStatManager.Inventory.playerStat.CurHp -= damage;

        if(PlayerStatManager.Inventory.playerStat.CurHp <= 0)
        {
            Die();
        }

        return false;
    }

    [ContextMenu("Heal")]
    public bool Heal()
    {
        if (PlayerStatManager.Inventory.FieldInventory.MedicalPoint == 0)
            return false;

        PlayerStatManager.Inventory.FieldInventory.MedicalPoint--;
        int targetHp = PlayerStatManager.Inventory.playerStat.CurHp + 35 < PlayerStatManager.Inventory.playerStat.MaxHp? PlayerStatManager.Inventory.playerStat.CurHp + 35 : PlayerStatManager.Inventory.playerStat.MaxHp;
        
        StartCoroutine(HealRoutine(PlayerStatManager.Inventory.playerStat.CurHp, targetHp));

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
            PlayerStatManager.Inventory.playerStat.CurHp += 1;

            if (PlayerStatManager.Inventory.playerStat.CurHp == targetHp)
                break;

            yield return new WaitForSeconds(0.03f);
        }
    }
}

