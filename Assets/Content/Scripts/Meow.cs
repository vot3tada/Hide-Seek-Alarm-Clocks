using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meow : MonoBehaviour
{
    // Start is called before the first frame update
    private double RandomValue;
    private AudioSource MeowSound;
    [SerializeField] private int RandomSeed; 
    void Start()
    {
        Random.InitState(RandomSeed);
        MeowSound = gameObject.GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(RandomValue * Random.value);
        if (Random.value > 0.999)
        {
            MeowSound.Play();
            //Debug.Log("Meow");
        }
    }
}
