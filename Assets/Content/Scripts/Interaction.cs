using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera mcCamera;
    
    private Ray hell;
    private RaycastHit hit;

    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject bag;

    private void FixedUpdate()
    {
        Ray();
        if (Input.GetAxis("Interaction") == 1)
        {
            Physics.Raycast(hell, out hit, maxDistance);
            if (hit.transform == null)
                return;
            switch (hit.transform.gameObject.tag)
            {
                case "Clock":
                    {
                        GameObject cat = hit.transform.gameObject;
                        cat.GetComponent<AudioSource>().Play();
                        cat.transform.parent = GameObject.FindGameObjectWithTag("Bag").transform;
                        cat.transform.localPosition = new Vector3(0f + 0.01f*bag.transform.childCount, 0f + 0.04f * bag.transform.childCount, 0f + -0.03f * bag.transform.childCount);
                        cat.transform.localRotation = Quaternion.Euler(0, -45, 0);
                        hit.transform.gameObject.GetComponent<CatEscape>().enabled = true;
                        break;
                    }
                case "PlaceForClocks":
                    {
                        for (int i = 1; i < bag.transform.childCount; i++)
                        {
                            //Нулевое - система частиц
                            GameObject cat = bag.transform.GetChild(1).gameObject;
                            cat.GetComponent<CatEscape>().enabled = false;
                            cat.transform.parent = hit.transform;
                            cat.transform.localPosition = new Vector3(2.5f - 1.2f * hit.transform.childCount, 12f, -2.7f + 0.75f * hit.transform.childCount);
                            cat.transform.localScale = new Vector3(0.3f, 0.5f, 1f);
                            cat.transform.localRotation = Quaternion.Euler(0, 180, 0);
                            cat.tag = "Untagged";
                        }
                        break;
                    }
                case "Door":
                    {
                        hit.transform.gameObject.GetComponentInParent<Open>().InteractionWithDoor();
                        break;
                    }
            }
        }
    }

    private void Ray() =>
        hell = mcCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));


}
