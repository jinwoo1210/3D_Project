using JetBrains.Annotations;
using System;

[Serializable]  // �賶 ������ ����Ʈ ����ü
public struct PackLevelUpPoint
{
    public int electPoint;
    public int toolPoint;
}

[Serializable]  // ���� ������ ����Ʈ ����ü
public struct StatLevelUpPoint
{
    public int medicalPoint;
    public int foodPoint;
}

[Serializable]  // �� ������ ����Ʈ ����ü
public struct GunLevelUpPoint
{
    public int electPoint;
    public int toolPoint;
}

[Serializable]
public struct GunStatLevel
{
    public int damageLevel;         // ������
    public int shootSpeedLevel;     // ���� �ӵ�
    public int magCapacityLevel;    // źâ ũ��
    public int reloadLevel;         // ���� �ӵ�
    public int fireDistanceLevel;   // ��Ÿ�

    public int totalLevel;
}

[Serializable]  // ���� ������
public struct Zombies
{
    public Monster prefab;
    public ZombieData data;
}

[Serializable]
public struct SpawnTable    // ������ ��������
{
    public int normalSpawnCount;
    public int elietSpawnCount;
    public int bossSpawnCount;
}

public struct PlayerInfo
{
    public int SMGLevel;            // SMG ����
    public int ARLevel;             // AR ����
    public int ShootGunLevel;       // SG ����
    public int SniperLevel;         // Sniper ����

    public int medicalPoint;        // �Ƿ� ����Ʈ
    public int medicalLevel;        // �Ƿ� �賶 ����
    public int foodPoint;           // �ķ� ����Ʈ
    public int foodLevel;           // �ķ� �賶 ����
    public int electPoint;          // ���� ����Ʈ
    public int electLevel;          // ���� �賶 ���� 
    public int toolPoint;           // ���� ����Ʈ
    public int toolLevel;           // ���� �賶 ����

    public int maxHp;               // �ִ� ü��
    public int maxStamina;          // �ִ� ���¹̳�
    public int moveSpeed;           // �̵� �ӵ�
}