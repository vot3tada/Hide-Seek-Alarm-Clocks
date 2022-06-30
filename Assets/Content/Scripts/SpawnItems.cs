using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] cats;
    [SerializeField] private GameObject[] items;
    private List<GameObject> spawns;
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        //Debug.Log(cats.Length);
        //Debug.Log(spawns.Count);
        for (int i = 0; i < 5; i++)
        {
            if (spawns.Count == 0)
            {
                break;
            }
            var rand = (int)Random.Range(0, spawns.Count);
            Instantiate(cats[i % cats.Length], spawns[rand].transform);
            spawns.RemoveAt(rand);
        }
        for (int i = 0; i < spawns.Count; i++)
        {
            if (Random.value > 0.5)
                Instantiate(items[Random.Range(0, items.Length)], spawns[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
