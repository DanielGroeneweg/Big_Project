using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;


    [SerializeField] private UnityEvent onStartButton;
    [SerializeField] private UnityEvent onSettingsButton;
    [SerializeField] private UnityEvent onApplicationQuit;
    
    public void Start()
    {
        startButton.onClick.AddListener(() => onStartButton?.Invoke());
        settingsButton.onClick.AddListener(() => onSettingsButton?.Invoke());
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
