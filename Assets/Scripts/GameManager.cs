using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    enum scenes { MainMenu, Gameplay }
    Scene currentScene;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDead += GoToMainMenu;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDead += GoToMainMenu;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene((int)scenes.Gameplay);
    }

    public void GoToMainMenu()
    {
        currentScene = SceneManager.GetSceneByBuildIndex((int)scenes.MainMenu);
    }
}
