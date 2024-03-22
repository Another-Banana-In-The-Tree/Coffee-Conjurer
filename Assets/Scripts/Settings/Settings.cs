using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{

    public static Settings Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private AudioManager audioManager;

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

        UpdateOutPut(volumeText, "100");
        UpdateOutPut(speedText, "x1");

        gameObject.SetActive(false);

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

}
