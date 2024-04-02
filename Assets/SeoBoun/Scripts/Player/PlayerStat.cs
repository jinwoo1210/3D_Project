using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    [SerializeField] int curHp;
    int curStamina;
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
            curStamina = value;
            ChangePlayerStamina?.Invoke(playerData.maxStamina, value);
        }
    }
    public int MoveSpeed { get { return moveSpeed; } }
    // ó�� �ʱ�ȭ ������ ����
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        curHp = playerData.maxHp;
        curStamina = playerData.maxStamina;
        moveSpeed = playerData.moveSpeed;
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
