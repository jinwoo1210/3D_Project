using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject loadingInterface;
    [SerializeField] Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        // LoadSceneAsnyc : 백그라운드에서 Scene을 비동기적으로 로딩
        // LoadSceneMode : 게임 플레이 장면과 함께 로딩되기 원할 경우 Additive로 설정

        scenesToLoad.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene"));
        scenesToLoad.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Part03", LoadSceneMode.Additive));    
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;

                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}