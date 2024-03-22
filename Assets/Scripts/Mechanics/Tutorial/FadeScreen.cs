using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    bool isFadingIn = true;
    public bool isFinished = false;
    bool isEnd = false;
    bool hasLoadedScene = false;
    private string levelLoad;
    private Animator animator;
    private void Update()
    {
        if (!isFinished && isEnd && !hasLoadedScene)
        {
            hasLoadedScene = true;
            SceneManager.LoadScene(levelLoad);
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        animator.SetBool("isFadingIn", true);
    }
    public void FadeOut()
    {
        animator.SetBool("isFadingIn", false);
    }
    public void FadeOutToLevel(string levelLoad)
    {
        animator.SetBool("isFadingIn", false);
        this.levelLoad = levelLoad;
        isEnd = true;
    }
    public bool GetIsFinished()
    {
        return isFinished;
    }
}
