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
        StartCoroutine(TryEscape());
    }

    private IEnumerator TryEscape()
    {
        while(true)
        {
            if (Random.value * timeFactor.Evaluate(dontescapecount) * bagfactor < PercentValue)
            {
                Debug.Log($"{name} {timeFactor.Evaluate(dontescapecount)} {dontescapecount} {bagfactor}");
                dontescapecount = 0;
                bagfactor = 1;
                var partic = Instantiate(particle, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
                partic.Play();
                Transform EscapePlace;
                var EscapePlaces = GameObject.FindGameObjectsWithTag("Spawner").Where(spawn => spawn.transform.childCount == 0).ToList();
                EscapePlace = EscapePlaces[(int)Random.Range(0, EscapePlaces.Count)].transform;
                gameObject.transform.position = EscapePlace.transform.position;

                gameObject.transform.parent = EscapePlace.transform;
                gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                gameObject.transform.localScale = Vector3.one * 0.1f;
            }
            else
            {
                dontescapecount += 1f;
                if (transform.parent.name == "Bag")
                {
                    bagfactor /= 1.04f;
                    Debug.Log(dontescapecount);
                }

            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResetDontEscapeCount()
    {
        dontescapecount = 0;
        bagfactor = 1;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
