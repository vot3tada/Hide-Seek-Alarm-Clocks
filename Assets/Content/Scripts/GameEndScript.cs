using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameEndScript : MonoBehaviour
{
    [SerializeField] DayCyrcleManager dayCyrcleManager;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject DefeatUI;
    [SerializeField] GameObject[] OtherUI;
    [SerializeField] TMP_Text mark;
    [SerializeField, Tooltip("Время в секундах"), Range(60,600)] private float gameSessionDuration;
    [SerializeField] AudioMixer audioMixer;
    private int catsFinded;
    private float timeLeft;
    private GameObject player;
    //private float ambientVolume;
    //private float soundsVolume;

    public float GameSessionDuration
    {
        get { return gameSessionDuration; }
        set 
        {
            if (value < 60 || value > 600)
                throw new System.Exception("Время игровой сессии выходит за границы.");
            gameSessionDuration = value;
            dayCyrcleManager.DayDuration = value;
        }
    }
    public void AddCat()
    {
        catsFinded++;
    }

    public int CatsFinded => catsFinded;

    public string TimerText => string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeLeft / 60), Mathf.FloorToInt(timeLeft % 60));

    void Start()
    {
        GameSessionDuration = PlayerPrefs.GetFloat("Duration");
        catsFinded = 0;
        player = GameObject.FindWithTag("Player");
        timeLeft = gameSessionDuration;
        StartCoroutine(StartTimer());
        //audioMixer.GetFloat("Sounds", out soundsVolume);
        //audioMixer.GetFloat("Ambient", out ambientVolume);
    }

    private IEnumerator StartTimer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            CheckEndGame();
            yield return null;
        }
    }

    private void CheckEndGame()
    {
        if(catsFinded == 5)
        {
            DefeatUI.SetActive(false);
            StopAllCoroutines();
            Cursor.lockState = CursorLockMode.None;
            foreach (GameObject l in OtherUI)
                l.SetActive(false);
            
            audioMixer.SetFloat("Sounds", -80f);
            audioMixer.SetFloat("Ambient", -80f);
            player.GetComponent<CharacterController>().enabled = false;

            mark.text = $"Ваша успеваемость: {3 + System.Math.Round((PlayerPrefs.GetFloat("CatActivity") * 1.15f) + (65.0f / PlayerPrefs.GetFloat("Duration")) + (PlayerPrefs.GetFloat("RoomsCount") * 0.05f),2)}";
            WinUI.SetActive(true);
        }
        if(timeLeft <= 0)
        {
            StopAllCoroutines();
            player.GetComponent<AudioSource>().Play();
            Cursor.lockState = CursorLockMode.None;
            foreach (GameObject l in OtherUI)
                l.SetActive(false);
            timeLeft = 0;
            audioMixer.SetFloat("Sounds", -80f);
            audioMixer.SetFloat("Ambient", -80f);
            player.GetComponent<CharacterController>().enabled = false;
            DefeatUI.SetActive(true);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
       //audioMixer.SetFloat("Sounds", soundsVolume);
        //audioMixer.SetFloat("Ambient", ambientVolume);
    }
}
