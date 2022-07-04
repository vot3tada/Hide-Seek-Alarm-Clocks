using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera mcCamera;
    
    private Ray hell;
    private RaycastHit hit;

    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject bag;

    private GameEndScript gameEndScript;

    private void Start()
    {
        gameEndScript = GameObject.Find("GameEndHandler").GetComponent<GameEndScript>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Interaction") == 1)
        {
            Ray();
            Physics.Raycast(hell, out hit, maxDistance);
            if (hit.transform == null)
                return;
            switch (hit.transform.gameObject.tag)
            {
                case "Clock":
                    {
                        GameObject cat = hit.transform.gameObject;
                        cat.GetComponent<AudioSource>().Play();
                        cat.transform.parent = GameObject.FindGameObjectWithTag("Bag").transform;
                        cat.transform.localPosition = new Vector3(0f + 0.01f*bag.transform.childCount, 0f + 0.04f * bag.transform.childCount, 0f + -0.03f * bag.transform.childCount);
                        cat.transform.localRotation = Quaternion.Euler(0, -45, 0);
                        cat.transform.localScale = Vector3.one * 0.1f;
                        //hit.transform.gameObject.GetComponent<CatEscape>().enabled = true;
                        hit.transform.gameObject.GetComponent<CatEscape>().ResetDontEscapeCount();
                        break;
                    }
                case "PlaceForClocks":
                    {
                        for (int i = 1; i < bag.transform.childCount; i++)
                        {
                            //Нулевое - система частиц
                            GameObject cat = bag.transform.GetChild(1).gameObject;
                            cat.GetComponent<CatEscape>().enabled = false;
                            cat.GetComponent<Meow>().enabled = false;
                            cat.transform.parent = hit.transform;
                            cat.transform.localPosition = new Vector3(3f - 1.2f * hit.transform.childCount, 12f, -3f + 0.75f * hit.transform.childCount);
                            cat.transform.localScale = new Vector3(0.3f, 0.5f, 1f);
                            cat.transform.localRotation = Quaternion.Euler(0, 180, 0);
                            cat.tag = "Untagged";
                            var audio = cat.GetComponents<AudioSource>();
                            audio[1].Stop();
                            audio[2].Play();
                            gameEndScript.AddCat();
                        }
                        break;
                    }
                case "Door":
                    {
                        hit.transform.gameObject.GetComponentInParent<Open>().InteractionWithDoor();
                        var audios = gameObject.GetComponents<AudioSource>().ToList();
                        audios.RemoveAt(0);
                        if (audios.Count(audio => audio.isPlaying) == 0)
                        {
                            audios[Random.Range(0, audios.Count)].Play();
                        }
                        break;
                    }
                case "Lighting":
                    {
                        var lamp = hit.transform.gameObject;
                        lamp.GetComponent<TurnOff>().SwitchLight();
                        if (!lamp.GetComponent<AudioSource>().isPlaying)
                        {
                            lamp.GetComponent<AudioSource>().Play();
                        }
                        break;
                    }
            }
        }
    }

    private void Ray() =>
        hell = mcCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));


}
