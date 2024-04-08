using System;
using UnityEngine;

public enum Stats { Hp, Stamina, MoveSpeed }
public class PlayerStat : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    int curHp;
    int curStamina;
    int maxHp;
    int maxStamina;
    int moveSpeed;

    public event Action<int, int> ChangePlayerHp;       // max, cur
    public event Action<int, int> ChangePlayerStamina;

    public int CurHp { 
        get 
        {
            return curHp; 
        }
        set
        {
            curHp = Mathf.Clamp(value, 0, maxHp);
            ChangePlayerHp?.Invoke(maxHp, value);
        }
    }

    public int CurStamina {
        get
        {
            return curStamina;
        }
        set
        {
            curStamina = Mathf.Clamp(value, 0, maxStamina);
            ChangePlayerStamina?.Invoke(maxStamina, value);
        }
    }
    public int MoveSpeed { get { return moveSpeed; } }
    public int MaxHp { get { return maxHp; } }
    public int MaxStamina { get { return maxStamina; } }
    // 처음 초기화 데이터 수정

    private void Start()
    {
        SetUp();
    }

    public void GameInit()
    {
        CurHp = MaxHp;
        CurStamina = MaxStamina;
    }

    public void FirstInit()
    {
        curHp = maxHp = playerData.maxHp;
        curStamina = maxStamina = playerData.maxStamina;
        moveSpeed = playerData.moveSpeed;
    }

    public void SetUp()
    {
        GameInit();
        ChangePlayerHp?.Invoke(playerData.maxHp, curHp);
        ChangePlayerStamina?.Invoke(playerData.maxStamina, curStamina);
    }

    public void AddChangeHp(Action<int, int> ChangeEvent)
    {
        ChangePlayerHp += ChangeEvent;
    }

    public void AddChangeStamina(Action<int, int> ChangeEvent)
    {
        ChangePlayerStamina += ChangeEvent;
    }
}
