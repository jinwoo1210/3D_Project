using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSpawner : MonoBehaviour
{
    // TODO... ������ ������� �����
    [SerializeField] protected BaseItem[] prefab;
    [SerializeField] protected int[] percentage;

    protected int rand;
    // Ȯ�� ���̺� ���� -> Ȯ�� ���̺��� ���� 1ȸ ����

    public abstract void Spawn();
}