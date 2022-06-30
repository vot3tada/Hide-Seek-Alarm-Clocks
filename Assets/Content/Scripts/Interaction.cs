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
        {
            hit.transform.gameObject.GetComponent<AudioSource>().Play();
            hit.transform.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Bag").transform;
            hit.transform.gameObject.transform.position = GameObject.FindGameObjectWithTag("Bag").transform.position;
            hit.transform.gameObject.GetComponent<CatEscape>().enabled = true;
            //Destroy(hit.transform.gameObject);
        }

        if (Physics.Raycast(hell, out hit, maxDistance) && hit.transform.gameObject.tag == "PlaceForClocks" && Input.GetAxis("Interaction") == 1)
        {
            var bag = GameObject.FindGameObjectWithTag("Bag");
            int count = bag.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var cat = GameObject.FindGameObjectWithTag("Bag").transform.GetChild(0);
                cat.gameObject.GetComponent<CatEscape>().enabled = false;
                cat.position = hit.transform.position;
                cat.parent = hit.transform.gameObject.transform;
                cat.tag = "Untagged";
            }
            
        }


        if (Physics.Raycast(hell, out hit, maxDistance) && hit.transform.gameObject.tag == "Door" && Input.GetAxis("Interaction") == 1)
        {
            hit.transform.gameObject.GetComponentInParent<Open>().InteractionWithDoor();
        }
    }

    private void Ray() =>
        hell = mcCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));


}
