using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{

    public static Settings Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private AudioManager audioManager;
    private bool isPaused = false;
    [SerializeField] private GameObject rendering;
    private int num;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        
                if (Instance != null && Instance != this)
                {
                    Destroy(this);
                }
                else
                {
                    Instance = this;
           
                }
        
        isPaused = false;
        rendering = null;
        rendering = transform.GetChild(0).gameObject;
        UpdateOutPut(volumeText, "100");
        UpdateOutPut(speedText, "x1");

        rendering.SetActive(false);
        rendering = transform.GetChild(0).gameObject;
        //PlayerInput.EnablePause();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePauseState();
        }
    }

    public void ChangeVolume(float newVol)
    {
        audioManager.SetVolume(newVol);
        string temp = (newVol * 100).ToString("F1");
        UpdateOutPut(volumeText, temp);
    }

    public void ChangeTextSpeed(float newSpeed)
    {
        DialogueManager.Instance.SetDialogueSpeed(newSpeed);
        string temp = "x " + (2f - (20 * newSpeed)).ToString("F1");
        UpdateOutPut(speedText,temp );
    }


    private void UpdateOutPut(TextMeshProUGUI text, string newVal)
    {
        text.text = newVal;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ChangePauseState()
    {
        print("pause state = " + isPaused);
        num++;
       // if (num % 2 == 0) return;
        if (this == null) return;
        if (isPaused)
        {
            print("unpause");
            Time.timeScale = 1;

        }
        else if(!isPaused)
        {
            print("pause");
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
        rendering.SetActive(isPaused);
    }
}
