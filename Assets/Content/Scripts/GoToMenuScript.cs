using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMenuScript : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private GameObject loadScrene;
    [SerializeField] private GameObject[] otherUI;
    private float progress = 0;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetButton("GoToMenu"))
        {
            progress += 0.01f;
            if (progress >= 1)
            {
                foreach (GameObject l in otherUI)
                    l.SetActive(false);
                loadScrene.SetActive(true);
                SceneManager.LoadSceneAsync(0);
            }  
        }
        else
            progress = 0;
        progressImage.fillAmount = progress;
    }
}
