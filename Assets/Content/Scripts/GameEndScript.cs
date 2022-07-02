using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameEndScript : MonoBehaviour
{
    [SerializeField] DayCyrcleManager dayCyrcleManager;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject DefeatUI;
    [SerializeField] GameObject[] OtherUI;
    [SerializeField, Tooltip("Время в секундах"), Range(60,600)] private float gameSessionDuration;
    [SerializeField] AudioMixer audioMixer;
    private int catsFinded;
    private float timeLeft;
    private GameObject player;

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
        catsFinded = 0;
        player = GameObject.FindWithTag("Player");
        timeLeft = gameSessionDuration;
        StartCoroutine(StartTimer());
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
            Cursor.lockState = CursorLockMode.None;
            foreach (GameObject l in OtherUI)
                l.SetActive(false);
            audioMixer.SetFloat("Sounds", -80f);
            audioMixer.SetFloat("Ambient", -80f);
            player.GetComponent<CharacterController>().enabled = false;
            WinUI.SetActive(true);
        }
        if(timeLeft <= 0)
        {
            player.GetComponent<AudioSource>().Play();
            Cursor.lockState = CursorLockMode.None;
            foreach (GameObject l in OtherUI)
                l.SetActive(false);
            audioMixer.FindSnapshot("EndGame").TransitionTo(1f);
            timeLeft = 0;
            player.GetComponent<CharacterController>().enabled = false;
            DefeatUI.SetActive(true);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        audioMixer.FindSnapshot("GameProcess").TransitionTo(1f);
    }
}
