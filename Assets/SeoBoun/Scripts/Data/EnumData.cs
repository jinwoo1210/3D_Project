// �� ����
public enum GunState { Ready, Empty, Shooting, Reloading, Size }                // �غ�, źâ�� �������, ���� ��(������), ���� ��

// ���� ����
public enum ZombieState { Idle, Trace, Attack, Return, Patrol, Die, Size }      // ���, �߰�, ����, �ǵ��ư���, ����, ����

// ���� ���� ����
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size }         // ���� ����, ���� ��, ���� ��(��Ÿ�� ����)

// ������ Ÿ��
public enum ItemType { Ammo, Medical, Food, Elect, Tool, Weapon, Size }         // źâ, �Ƿ�, �ķ�, ����, ����, ����(��� ����)

// �������ͽ� Ÿ��
public enum Stats { Hp, Stamina, MoveSpeed, Size }                              // ü��, ���¹̳�(�޸��� �� �̿�), ���ǵ�

// ���� Ÿ��    
public enum WeaponType { SMG, AR, SG, SR, Size }                                // SMG, AR, SG, SR Ÿ��