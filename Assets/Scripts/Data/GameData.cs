using System;

[Serializable]
public class GameData
{
    public PlayerInfo playerData;
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