using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void nextScene(string sceneName)
    {
        //GameManager.instance.UpdateGameState(GameState.menu);
        SceneManager.LoadScene(sceneName);
    }


    public void quitGame()
    {
        Application.Quit();
        Debug.Log("shit is closed it just dont show cause it dont work in the editor");
    }
}
