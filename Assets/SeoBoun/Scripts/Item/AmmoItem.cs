using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : BaseItem
{
    public override void Interactor(PlayerInteractor player)
    {
        // TODO... ź�෮ �ø��� �÷��̾��� �� ������ ã��, ã�� ������ �������� ź�෮ ����
        
        Destroy(gameObject);
    }
}
