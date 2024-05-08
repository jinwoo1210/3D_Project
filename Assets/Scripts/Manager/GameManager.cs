using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPos;
    public WeaponHolder holder;
    private void Start()
    {
        if (GameObject.FindWithTag("Player") == null)
            return;

        holder = GameObject.FindObjectOfType<WeaponHolder>();
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    public void FindTarget()
    {
        holder = GameObject.FindObjectOfType<WeaponHolder>();
        playerPos = GameObject.FindWithTag("Player").transform;
    }
}
