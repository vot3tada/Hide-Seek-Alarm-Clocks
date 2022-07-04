using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isStopped || particle.isPaused || !particle.IsAlive())
        {
            Destroy(gameObject);
            //particle.GetComponent<DestroyParticleSystem>().enabled = false;
        }
    }
}
