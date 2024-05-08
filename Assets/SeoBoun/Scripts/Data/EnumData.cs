// 총 상태
public enum GunState { Ready, Empty, Shooting, Reloading, Size }                // 준비, 탄창이 비어있음, 슈팅 중(딜레이), 장전 중

// 좀비 상태
public enum ZombieState { Idle, Trace, Attack, Return, Patrol, Die, Size }      // 대기, 추격, 공격, 되돌아가기, 정찰, 죽음

// 좀비 어택 상태
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size }         // 어택 시작, 어택 중, 어택 끝(쿨타임 시작)

// 아이탬 타입
public enum ItemType { Ammo, Medical, Food, Elect, Tool, Weapon, Size }         // 탄창, 의료, 식량, 전자, 도구, 무기(사용 안함)

// 스테이터스 타입
public enum Stats { Hp, Stamina, MoveSpeed, Size }                              // 체력, 스태미나(달리기 시 이용), 스피드

// 웨폰 타입    
public enum WeaponType { SMG, AR, SG, SR, Size }                                // SMG, AR, SG, SR 타입