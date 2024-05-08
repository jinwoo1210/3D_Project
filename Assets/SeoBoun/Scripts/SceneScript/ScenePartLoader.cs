using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePartLoader : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float loadRange;

    private bool isLoaded;

    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.sceneCount > 0)
        {
            for(int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        if(Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }

    private void LoadScene()
    {
        if(!isLoaded)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void UnLoadScene()
    {
        if(isLoaded)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}
