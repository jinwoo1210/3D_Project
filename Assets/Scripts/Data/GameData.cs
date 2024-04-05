using System;

[Serializable]
public class GameData
{
    public PlayerInfo playerData;
}
public struct PlayerInfo
{
    public int SMGLevel;            // SMG 레벨
    public int ARLevel;             // AR 레벨
    public int ShootGunLevel;       // SG 레벨
    public int SniperLevel;         // Sniper 레벨

    public int medicalPoint;        // 의료 포인트
    public int medicalLevel;        // 의료 배낭 레벨
    public int foodPoint;           // 식량 포인트
    public int foodLevel;           // 식량 배낭 레벨
    public int electPoint;          // 전자 포인트
    public int electLevel;          // 전자 배낭 레벨 
    public int toolPoint;           // 도구 포인트
    public int toolLevel;           // 도구 배낭 레벨

    public int maxHp;               // 최대 체력
    public int maxStamina;          // 최대 스태미나
    public int moveSpeed;           // 이동 속도
}