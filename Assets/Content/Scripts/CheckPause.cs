using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    void Update()
    {
        if(Input.GetButtonUp("Pause"))
            pauseUI.SetActive(!pauseUI.activeSelf);
    }
}
