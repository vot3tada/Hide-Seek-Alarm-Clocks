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
    //[SerializeField] 
    private double PercentValue;
    [SerializeField] AnimationCurve timeFactor;
    private float dontescapecount = 0;
    private float bagfactor = 1;
    
    private void Awake()
    {
        //particle.GetComponent<Renderer>().material = material;
        PercentValue = PlayerPrefs.GetFloat("CatActivity");
        Debug.Log(PercentValue);
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time % EveryFrame == 0)
        { 
            if (Random.value * timeFactor.Evaluate(dontescapecount) * bagfactor < PercentValue)
            {
                Debug.Log($"{name} {timeFactor.Evaluate(dontescapecount)} {dontescapecount} {bagfactor}");
                dontescapecount = 0;
                bagfactor = 1;
                //��� ��������� ����, 28 ������ - �������
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
                
                gameObject.transform.parent = EscapePlace.transform;
                gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                gameObject.transform.localScale = Vector3.one * 0.1f;
                //gameObject.GetComponent<CatEscape>().enabled = false;
            }
            else
            {
                dontescapecount += 1f;
                if (transform.parent.name == "Bag")
                {
                    bagfactor /= 1.01f;
                    Debug.Log(dontescapecount);
                }
                    
            }
        }
    }

    public void ResetDontEscapeCount()
    {
        dontescapecount = 0;
        bagfactor = 1;
    }
}
