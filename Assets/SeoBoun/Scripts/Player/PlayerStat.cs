using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    [SerializeField] int curHp;
    [SerializeField] int curStamina;
    int moveSpeed;
    int runSpeed;

    public event Action<int, int> ChangePlayerHp;       // max, cur
    public event Action<int, int> ChangePlayerStamina;

    public int CurHp { 
        get 
        {
            return curHp; 
        }
        set
        {
            curHp = Mathf.Clamp(value, 0, playerData.maxHp);
            ChangePlayerHp?.Invoke(playerData.maxHp, value);
        }
    }

    public int CurStamina {
        get
        {
            return curStamina;
        }
        set
        {
            curStamina = Mathf.Clamp(value, 0, playerData.maxStamina);
            ChangePlayerStamina?.Invoke(playerData.maxStamina, value);
        }
    }
    public int MoveSpeed { get { return moveSpeed; } }
    public int RunSpeed { get { return runSpeed; } }
    public int MaxHp { get { return playerData.maxHp; } }
    // 처음 초기화 데이터 수정
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        curHp = playerData.maxHp;
        curStamina = playerData.maxStamina;
        moveSpeed = playerData.moveSpeed;
        runSpeed = playerData.runSpeed;
    }

    [ContextMenu("SetUp")]
    public void SetUp()
    {
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
