using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

[Serializable]
public class MenuManager : MonoBehaviour
{
    [SerializeField] string _tutorialSceneName;
    [SerializeField] string _levelOneSceneName;

    [SerializeField] Light2D transitionLight;
    [SerializeField] float endRadius = 20;
    [SerializeField] float transitionSpeed = 1;
    bool isTransitionFinished = false;
    bool isStartPressed = false;
    
    bool isSkip = false;
    bool isSkipPressed = false;

    [SerializeField] GameObject[] skipDialogueHide;
    [SerializeField] GameObject[] skipDialogueShow;

    private void Awake()
    {
        if (transitionLight != null) transitionLight.pointLightOuterRadius = 0;
    }
    private void Update()
    {
        TransitionLight();
        //if (isTransitionFinished) transitionLight.pointLightOuterRadius = 0;
        if (isTransitionFinished && isStartPressed && isSkip)
        {
            Debug.Log("Loading " + _tutorialSceneName);
            SceneManager.LoadScene(_tutorialSceneName);
        }
        else if (isTransitionFinished && isStartPressed)
        {
            Debug.Log("Loading " + _levelOneSceneName);
            SceneManager.LoadScene(_levelOneSceneName);
        }
        //Debug.Log(transitionLight.pointLightOuterRadius);
    }
    public void SwitchScene(bool skip)
    {
        isStartPressed = true;
        isSkip = skip;
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
    public void ShowSkip()
    {
        for (int i = 0; i < skipDialogueHide.Length; i++)
        {
            Debug.Log("Hiding " + skipDialogueHide[i].name);
            skipDialogueHide[i].SetActive(false);
        }
        for (int i = 0; i < skipDialogueShow.Length; i++)
        {
            Debug.Log("Showing " + skipDialogueShow[i].name);
            skipDialogueShow[i].SetActive(true);
        }
    }
}
