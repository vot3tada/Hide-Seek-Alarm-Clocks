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
        for (int i = 0; i < cats.Length; i++)
        {
            if (spawns.Count == 0)
            {
                break;
            }
            var rand = (int)Random.Range(0, spawns.Count);
            GameObject gmNew = Instantiate(cats[i], spawns[rand].transform.position, Quaternion.identity);
            gmNew.transform.parent = spawns[rand].transform;
            spawns.RemoveAt(rand);
        }
        for (int i = 0; i < spawns.Count; i++)
        {
            if (Random.value > 0.66)
            {
                GameObject gmNew = Instantiate(items[Random.Range(0,items.Length)], spawns[i].transform.position, Quaternion.identity);
                gmNew.transform.parent = spawns[i].transform;
                spawns.RemoveAt(i);
                i--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
