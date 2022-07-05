using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        loadScreen.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }
}
