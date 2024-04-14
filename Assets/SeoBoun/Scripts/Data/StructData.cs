using JetBrains.Annotations;
using System;

[Serializable]  // 배낭 레벨업 포인트 구조체
public struct PackLevelUpPoint
{
    public int electPoint;
    public int toolPoint;
}

[Serializable]  // 스탯 레벨업 포인트 구조체
public struct StatLevelUpPoint
{
    public int medicalPoint;
    public int foodPoint;
}

[Serializable]  // 건 레벨업 포인트 구조체
public struct GunLevelUpPoint
{
    public int electPoint;
    public int toolPoint;
}

[Serializable]
public struct GunStatLevel
{
    public int damageLevel;         // 데미지
    public int shootSpeedLevel;     // 연사 속도
    public int magCapacityLevel;    // 탄창 크기
    public int reloadLevel;         // 장전 속도
    public int fireDistanceLevel;   // 사거리

    public int totalLevel;
}

[Serializable]  // 좀비 데이터
public struct Zombies
{
    public Monster prefab;
    public ZombieData data;
}

[Serializable]
public struct SpawnTable    // 레벨별 스폰정보
{
    public int normalSpawnCount;
    public int elietSpawnCount;
    public int bossSpawnCount;
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