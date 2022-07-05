using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WatchTrailer : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToTrailer()
    {
        SceneManager.LoadScene(0);
    }
}
