using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button[] startButtons;
    [SerializeField] private Button[] settingsButtons;
    [SerializeField] private Button[] quitButtons;


    [SerializeField] private UnityEvent onStartButton;
    [SerializeField] private UnityEvent onSettingsButton;
    [SerializeField] private UnityEvent onApplicationQuit;
    
    public void Start()
    {
        foreach (Button startButton in startButtons)
            startButton.onClick.AddListener(() => onStartButton?.Invoke());
        
        foreach (Button settingsButton in settingsButtons)
            settingsButton.onClick.AddListener(() => onSettingsButton?.Invoke());
        
        foreach (Button quitButton in quitButtons)
            quitButton.onClick.AddListener(() => onApplicationQuit?.Invoke());
    }
    public void Quit()
    {
        Debug.Log("Quitting the application...");
        Application.Quit();
    }
    public void LoadScene(int pSceneIndex)
    {
        SceneManager.LoadScene(pSceneIndex);
    }

    public void LoadScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

    public void SetVolume(float volume)
    {
        //connect the mixer to this method later
        //mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
