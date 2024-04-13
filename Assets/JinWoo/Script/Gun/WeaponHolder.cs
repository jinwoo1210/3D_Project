using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponType { SMG, AR, SG, SR, Size}
public class WeaponHolder : MonoBehaviour
{
    // ������ �� ����Ʈ
    [SerializeField] Gun[] guns;
    [SerializeField] BulletUI bulletUI;

    // ���� ����ϰ� �ִ� ��
    Gun curEquipGun;

    public Gun CurEquipGun { get { return curEquipGun; } }

    private void Awake()
    {
        InitFirst();
    }

    public void InitFirst()
    {
        for(int i = 0; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(false);
        }

        ChangeWeapon(WeaponType.SMG);
    }

    private void OnSetSMG(InputValue value)
    {
        // 0 : SMG
        ChangeWeapon(WeaponType.SMG);
    }

    private void OnSetAR(InputValue value)
    {
        // 1 : AR
        ChangeWeapon(WeaponType.AR);
    }

    private void OnSetSG(InputValue value)
    {
        // 2 : SG
        ChangeWeapon(WeaponType.SG);
    }

    private void OnSetSR(InputValue value)
    {
        ChangeWeapon(WeaponType.SR);
    }    

    private void ChangeWeapon(WeaponType type)
    {
        if (curEquipGun != null && (curEquipGun.GunState == State.Reloading || curEquipGun.GunState == State.Shooting)) // �������̰ų� ���� �����̿��� ���� �Ұ���
            return;

        if (curEquipGun != null)
        {
            curEquipGun.gameObject.SetActive(false);
        }

        curEquipGun = guns[(int)type];
        bulletUI.SetUp();
        curEquipGun.EventInvoke();
        curEquipGun.gameObject.SetActive(true);
    }
}
