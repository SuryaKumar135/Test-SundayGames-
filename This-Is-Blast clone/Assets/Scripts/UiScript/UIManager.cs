using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Canvas References")]
    [SerializeField] private GameObject levelCompletionCanvas;        
    [SerializeField] private GameObject GameComplete;

    private void Awake()
    {
        // Implement the Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void ShowLevelCompletion()
    {
        if (levelCompletionCanvas != null)
        {
            levelCompletionCanvas.SetActive(true);
        }
    }
    

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

       
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            levelCompletionCanvas.SetActive(false);
        }
        else
        {
            GameComplete.SetActive(true);
            //Debug.Log("No more levels. Game completed!");
        }
    }

    public void EnableNextLevelMenu()
    {
        levelCompletionCanvas.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //Debug.Log("Game Quit");
        Application.Quit();
    }
}
