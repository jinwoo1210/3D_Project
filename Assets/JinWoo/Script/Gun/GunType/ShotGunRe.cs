using UnityEngine;

public class ShotGunRe : Gun
{
    public Transform[] bulletSpawner;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Shoot()
    {
        // 실제 내부에서 발사
        if (player.equipWeaponIndex == -1 ||    // 플레이어가 아무것도 장비하지 않았거나(-1),
            state == State.Empty)               // 총이 비어있는 상태(Empty)라면
            return;                             // 밑의 문장을 실행하지 않고 종료(return)

        Vector3 pos = muzzlePoint.forward;
        pos.y = 0f;

        // 맞지 않더라도, 총구에서 화염는 항상 나오며
        muzzleFlash.Play();

        // 트레일 렌더러 활성화
        for (int i = 0; i < bulletSpawner.Length; i++)      // 샷건이기에 총알생성을 배열로 만들어둠.
        {
            // 트레일을 그릴 총알을 생성하고
            GameObject instantBullet = Instantiate(bulletObject, bulletSpawner[i].position, bulletSpawner[i].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();

            // 해당 총알에게 앞 방향으로 가속주기
            bulletRigid.velocity = bulletSpawner[i].forward * 50;
        }

        // 한 발 발사할 때 마다 현재 사용중인 탄약 줄이기
        curAmmo--;

        if (curAmmo <= 0)       // 만약 현재 잔탄이 0이면 
        {
            state = State.Empty;    // 총이 비어 있는 상태(Empty)로 전환
        }
    }
}
