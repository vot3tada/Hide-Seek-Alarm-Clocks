using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatEscape : MonoBehaviour 
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem particle;
    //[SerializeField] Material material;
    [SerializeField] int EveryFrame;
    [SerializeField] double PercentValue;
    [SerializeField] AnimationCurve timeFactor;
    private float dontescapecount = 0;
    private void Awake()
    {
        //particle.GetComponent<Renderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time % EveryFrame == 0)
        {
            if (Random.value * timeFactor.Evaluate(dontescapecount) < PercentValue)
            {
                dontescapecount = 0;
                //Для вылетания кота, 28 строка - коммент
                //var partic = gameObject.transform.Find("EscapingCat").GetComponent<ParticleSystem>();
                //partic.GetComponent<Renderer>().material = material;
                //var main = partic.main;
                //main.maxParticles = 1;
                //main.loop = false;
                var partic = Instantiate(particle, gameObject.transform.position,Quaternion.Euler(-90,0,0));
                partic.Play();
                Transform EscapePlace;
                var EscapePlaces = GameObject.FindGameObjectsWithTag("Spawner").Where(spawn => spawn.transform.childCount == 0).ToList();
                EscapePlace = EscapePlaces[(int)Random.Range(0, EscapePlaces.Count)].transform;
                gameObject.transform.position = EscapePlace.transform.position;
                gameObject.transform.localScale = Vector3.one * 0.1f;
                gameObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                gameObject.transform.parent = EscapePlace.transform;
                //gameObject.GetComponent<CatEscape>().enabled = false;
            }
            else
                dontescapecount += 1f;

        }
    }

    public void ResetDontEscapeCount()
    {
        dontescapecount = 0;
    }
}
