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
    float moveSpeed;

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
    public float MoveSpeed { get { return moveSpeed; } }
    public int MaxHp { get { return maxHp; } }
    public int MaxStamina { get { return maxStamina; } }
    // 처음 초기화 데이터 수정

    private void Start()
    {
        FirstInit();
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

    public void LevelUp()
    {
        maxHp = 100 + PlayerStatManager.Inventory.hpLevel * 50;
        maxStamina = 100 + PlayerStatManager.Inventory.staminaLevel * 30;
        moveSpeed = 3.0f + PlayerStatManager.Inventory.speedLevel * 0.5f;
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
