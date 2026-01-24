using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private PlayerMovement playerMovement;

    private bool isPaused = false;

    private void Start()
    {
        pauseUI.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseUI.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 🔑 MISSING LINE (THIS IS THE BUG)
        playerMovement.canMove = false;
    }

    public void Resume()
    {
        isPaused = false;
        pauseUI.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovement.canMove = true;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



