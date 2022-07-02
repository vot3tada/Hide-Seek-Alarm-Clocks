using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCats : MonoBehaviour
{
    [SerializeField] private GameEndScript gameEndScript;
    [SerializeField] private TextMeshProUGUI text;

    void Update()
    {
        text.text = $"Найдено: {gameEndScript.CatsFinded}/5";
    }
}
