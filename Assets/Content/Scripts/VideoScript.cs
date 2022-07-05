using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    void Update()
    {
        if ((GetComponentInChildren<VideoPlayer>().isPrepared && GetComponentInChildren<VideoPlayer>().isPaused) || Input.GetButtonUp("Pause"))
            SceneManager.LoadScene(1);
    }
}
