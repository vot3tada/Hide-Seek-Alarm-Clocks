using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEscape : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem particle;
    [SerializeField] Material material;
    private void Awake()
    {
        particle.GetComponent<Renderer>().material = material;
    }
    void Start()
    {
        //particle.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time % 10 == 0)
        {
            if (Random.value < 0.1)
            {
                var partic = Instantiate(particle, GameObject.FindGameObjectWithTag("Bag").transform);
                partic.GetComponent<Renderer>().material = material;
                var main = partic.main;
                main.maxParticles = 1;
                main.loop = false;
                //partic.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
                Destroy(gameObject);
            }
        }
    }
}
