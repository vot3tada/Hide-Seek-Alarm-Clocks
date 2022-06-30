using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rendering : MonoBehaviour
{
    //Этот код накладывается на камеру, должен был пойти в угоду оптимизации, в итоге только все портирт, хотя да, отрисовку лишнего убирает
    private Camera camera;
    private bool IsVisible(Transform item)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(item.position) < 0)
            {
                return false;
            }
        }
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var renders = FindObjectsOfType<Renderer>();
        foreach (var item in renders)
        {
            if (IsVisible(item.transform))
            {
                item.enabled = true;
            }
            else
            {
                item.enabled = false;
            }
        }
    }
}
