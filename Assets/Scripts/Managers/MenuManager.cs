using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MenuManager : MonoBehaviour
{
    Scene scene;
    public string _sceneName;
    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
