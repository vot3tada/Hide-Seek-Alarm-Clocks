using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void exit_game()
    {
        Application.Quit();
        Debug.Log("Выхожу");
    }

    public void new_game()
    {
        SceneManager.LoadScene(1);
    }
}
