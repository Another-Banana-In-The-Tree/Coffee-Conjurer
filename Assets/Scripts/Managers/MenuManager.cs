using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

[Serializable]
public class MenuManager : MonoBehaviour
{
    Scene scene;
    public string _sceneName;
    [SerializeField] Light2D transitionLight;
    [SerializeField] float endRadius = 20;
    [SerializeField] float transitionSpeed = 1;
    bool isTransitionFinished = false;
    bool isStartPressed = false;

    private void Awake()
    {
        transitionLight.pointLightOuterRadius = 0;
    }
    private void Update()
    {
        TransitionLight();
        if (isTransitionFinished && isStartPressed)
        {
            Debug.Log("Loading " + _sceneName);
            SceneManager.LoadScene(_sceneName);
        }
        Debug.Log(transitionLight.pointLightOuterRadius);
    }
    public void SwitchScene()
    {
        isStartPressed = true;
        isTransitionFinished = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void TransitionLight()
    {
        isTransitionFinished = false;
        if (!isStartPressed && !isTransitionFinished)
        {
            if (transitionLight.pointLightOuterRadius <= endRadius)
            {
                transitionLight.pointLightOuterRadius += transitionSpeed * Time.deltaTime;
            }
            else isTransitionFinished = true;
        }
        if (isStartPressed && !isTransitionFinished)
        {
            if (transitionLight.pointLightOuterRadius >= 0)
            {
                transitionLight.pointLightOuterRadius -= transitionSpeed * Time.deltaTime;
            }
            else if (transitionLight.pointLightOuterRadius <= 0) isTransitionFinished = true;
        }
    }
}
