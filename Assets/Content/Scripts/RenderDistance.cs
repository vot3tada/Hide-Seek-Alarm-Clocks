using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RenderDistance : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    [SerializeField, Range(20, 100)] float renderDistance;
    private List<GameObject> renders;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        renders = GameObject.FindGameObjectsWithTag("Forniture").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in renders)
        {
            MeshRenderer render;
            if (obj == null) continue;
            obj.TryGetComponent<MeshRenderer>(out render);
            if (Vector3.Distance(player.position, obj.transform.position) >= renderDistance)
            {
                if (render != null)
                {
                    render.enabled = false;
                }
                else
                {
                    obj.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(child => child.enabled = false);
                    obj.GetComponentsInChildren<Renderer>().ToList().ForEach(child => child.enabled = false);
                }
            }
            else
            {
                if (render != null)
                {
                    render.enabled = true;
                }
                else
                {
                    obj.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(child => child.enabled = true);
                    obj.GetComponentsInChildren<Renderer>().ToList().ForEach(child => child.enabled = true);
                } 
            }
        }
        //Debug.Log(renders.Count);
    }
}
