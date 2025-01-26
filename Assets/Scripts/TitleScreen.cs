using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The name of the scene to load when a key is pressed.")]
    public string sceneToLoad;

    [Tooltip("The key to press to start the game.")]
    public KeyCode startKey = KeyCode.Space;

    void Update()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(startKey))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Ensure the scene name is set
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the TitleScreen script.");
        }
    }
}
