using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : BaseItem
{
    public override void Interactor(PlayerInteractor player)
    {
        // TODO... 탄약량 늘리기 플레이어의 건 정보를 찾고, 찾은 정보를 바탕으로 탄약량 증가
        Manager.Game.holder.CurEquipGun.AmmoRemain += Manager.Game.holder.CurEquipGun.GunData.gunLevelData[0].magCapacity;

        Destroy(gameObject);
    }
}
