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
    public event Action ChangeMaxStamina;
    public event Action ChangeMoveSpeed;

    public event Action ChangeCurHp;
    public event Action ChangeCurStamina;
    public event Action ChangeCurMoveSpeed;

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
            ChangeMaxStamina?.Invoke();
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
            ChangeMoveSpeed?.Invoke();
            moveSpeed = value;
        }
    }

}
