using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDistance : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] ParticleSystem render;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        render = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) >= 100)
        {
            render.Stop(render, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        else
        {
            render.Play();
        }
    }
}
