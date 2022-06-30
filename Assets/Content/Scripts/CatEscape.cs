using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatEscape : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem particle;
    [SerializeField] Material material;
    [SerializeField] int EveryFrame;
    [SerializeField] double PercentValue;
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
        if ((int)Time.time % EveryFrame == 0)
        {
            if (Random.value < PercentValue)
            {
                var partic = GameObject.FindGameObjectWithTag("Player").transform.Find("EscapingCat").GetComponent<ParticleSystem>();
                partic.GetComponent<Renderer>().material = material;
                var main = partic.main;
                main.maxParticles = 1;
                main.loop = false;
                partic.Play();
                //partic.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
                Transform EscapePlace;
                if (Random.value > 0.5)
                {
                    EscapePlace = GameObject.FindGameObjectsWithTag("Spawner").FirstOrDefault(spawn => spawn.transform.childCount == 0).transform;
                }
                else
                {
                    EscapePlace = GameObject.FindGameObjectsWithTag("Spawner").LastOrDefault(spawn => spawn.transform.childCount == 0).transform;
                }
                gameObject.transform.position = EscapePlace.transform.position;
                //gameObject.transform.localScale = Vector3.one;
                gameObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                gameObject.transform.parent = EscapePlace.transform;
                gameObject.GetComponent<CatEscape>().enabled = false;
            }
        }
    }
}
