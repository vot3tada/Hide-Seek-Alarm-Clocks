using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[(int)(Time.time * 10) % sprites.Length];
    }
}
