using UnityEngine;

public class Level : MonoBehaviour
{
    public GunData gunData;
    public Gun gun;
    public int level = 0;

    private void Update()
    {
        LevelUp();
    }

    void LevelUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((level <= gunData.datas.Length))
            {
                level++;
                gun.Setup(level);
                Debug.Log(level);
            }
            else
            {
                return;
            }
        }
    }
}
