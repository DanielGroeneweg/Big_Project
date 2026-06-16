using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Name of the scene you want to load")]
    [SerializeField] string sceneToLoad;

    [Tooltip("Time before loading the next scene.")]
    [SerializeField] float loadingTime;

    [SerializeField] UnityEvent onLoadScene;

    public enum ButtonType { Quit, LoadScene }
    [Tooltip("Button's purpose")] public ButtonType buttonType;

    private IEnumerator OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.Quit:
                //Debug.Log("Quit Game");
                Application.Quit();
                break;
            case ButtonType.LoadScene:
                onLoadScene.Invoke();
                //Debug.Log("Loading Scene");
                yield return new WaitForSeconds(loadingTime);
                SceneManager.LoadSceneAsync(sceneToLoad);
                break;
            default:
                break;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Wait(sceneName));

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Wait(string sceneName)
    {
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
