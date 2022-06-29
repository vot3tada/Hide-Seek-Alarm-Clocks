using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera mcCamera;
    
    private Ray hell;
    private RaycastHit hit;

    [SerializeField] private float maxDistance;

    private void FixedUpdate()
    {
        Ray();
        if (Physics.Raycast(hell, out hit, maxDistance) && hit.transform.gameObject.tag == "Clock" && Input.GetAxis("Interaction") == 1)
            Destroy(hit.transform.gameObject);
        if (Physics.Raycast(hell, out hit, maxDistance) && hit.transform.gameObject.tag == "Door" && Input.GetAxis("Interaction") == 1)
        {
            hit.transform.gameObject.GetComponentInParent<Open>().InteractionWithDoor();
        }
    }

    private void Ray() =>
        hell = mcCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));


}
