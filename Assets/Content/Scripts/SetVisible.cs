using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisible : MonoBehaviour
{

    void Update()
    {
        if (gameObject.GetComponent<CanvasGroup>().alpha < 1)
            gameObject.GetComponent<CanvasGroup>().alpha += 0.01f;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

}
