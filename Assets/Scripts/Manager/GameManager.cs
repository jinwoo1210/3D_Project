using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPos;

    private void Start()
    {
        if (GameObject.FindWithTag("Player") == null)
            return;

        playerPos = GameObject.FindWithTag("Player").transform;
        playerPos.GetComponent<PlayerStat>().FirstInit();
    }
}
