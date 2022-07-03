using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void exit_game()
    {
        Application.Quit();
        Debug.Log("Выхожу");
    }

    public void new_game()
    {
        
    }
}
