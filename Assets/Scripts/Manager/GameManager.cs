using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
}
