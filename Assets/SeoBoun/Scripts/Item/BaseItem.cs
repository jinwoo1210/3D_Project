using UnityEngine;

public class BaseItem : MonoBehaviour, IInteractable
{
    // 모든 아이템이 가질 베이스 클래스
    [SerializeField] protected ItemType type;
    [SerializeField] protected int cost;


    protected void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime, Space.World);
    }

    public virtual void Interactor(PlayerInteractor player)
    {
        // 자식 함수에서 구현해야 함
    }
}
