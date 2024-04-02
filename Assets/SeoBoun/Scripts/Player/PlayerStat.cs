using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] int maxHp;         // 기본값 100
    [SerializeField] int maxStamina;    // 기본값 100
    [SerializeField] int moveSpeed;     // 기본값 3

    [SerializeField] int curHp;
    [SerializeField] int curStamina;
    [SerializeField] int curMoveSpeed;

    public event Action<int> ChangeMaxHp;
    public event Action<int> ChangeMaxStamina;
    public event Action<int> ChangeMoveSpeed;

    public event Action<int> ChangeCurHp;
    public event Action<int> ChangeCurStamina;
    public event Action<int> ChangeCurMoveSpeed;

    public int MaxHP { 
        get 
        { 
            return maxHp; 
        } 
        set 
        {
            ChangeMaxHp?.Invoke(value);
            maxHp = value; 
        }    
    }

    public int MaxStamina {
        get
        {
            return maxStamina;
        }
        set
        {
            ChangeMaxStamina?.Invoke(value);
            maxStamina = value;
        }
    }
    public int MaxMoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            ChangeMoveSpeed?.Invoke(value);
            moveSpeed = value;
        }
    }
    public int CurHp
    {
        get
        {
            return curHp;
        }
        set
        {
            ChangeCurHp?.Invoke(value);
            curHp = value;
        }
    }
    public int CurStamina
    {
        get
        {
            return curStamina;
        }
        set
        {
            ChangeCurStamina?.Invoke(value);
            curStamina = value;
        }
    }

    // 초기화 세팅
    public void Init()
    {
        curHp = MaxHP;
        curStamina = MaxStamina;
        curMoveSpeed = MaxMoveSpeed;
    }
}
