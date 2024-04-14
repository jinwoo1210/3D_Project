using UnityEngine;

public class BaseItem : MonoBehaviour, IInteractable
{
    // ��� �������� ���� ���̽� Ŭ����
    [SerializeField] protected ItemType type;
    [SerializeField] protected int cost;


    protected void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime, Space.World);
    }

    public virtual void Interactor(PlayerInteractor player)
    {
        // �ڽ� �Լ����� �����ؾ� ��
    }
}
