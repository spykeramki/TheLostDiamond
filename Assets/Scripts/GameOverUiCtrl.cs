using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiCtrl : MonoBehaviour
{
    public void OnClickHomeBtn()
    {
        SceneManager.LoadSceneAsync("00Start");
    }

    public void OnClickRestartBtn()
    {
        SceneManager.LoadSceneAsync("01Main");
    }
}
