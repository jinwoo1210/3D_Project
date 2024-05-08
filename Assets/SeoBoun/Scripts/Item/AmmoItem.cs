using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : BaseItem
{
    public override void Interactor(PlayerInteractor player)
    {
        // TODO... ź�෮ �ø��� �÷��̾��� �� ������ ã��, ã�� ������ �������� ź�෮ ����
        int amount = Manager.Game.holder.CurEquipGun.MagCapacity;

        Manager.Game.holder.CurEquipGun.AmmoRemain += amount;

        Destroy(gameObject);
    }
}
