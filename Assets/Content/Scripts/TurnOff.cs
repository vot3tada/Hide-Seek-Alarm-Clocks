using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    [SerializeField] private Light light;
    private float lastTime;
    private Renderer lamp;
    private Color turnOnColor;

    // Start is called before the first frame update
    void Start()
    {
        light.enabled = false;
        lastTime = Time.time;
        lamp = light.transform.parent.GetComponent<MeshRenderer>();
        turnOnColor = lamp.materials[1].GetColor("_EmissionColor");
        lamp.materials[1].SetColor("_EmissionColor", Color.black);
    }

    // Update is called once per frame
    public void SwitchLight()
    {
        if (Time.time - lastTime > 0.4f)
        {
            light.enabled = !light.enabled;
            if (light.enabled)
            {
                lamp.materials[1].SetColor("_EmissionColor", turnOnColor);
            }
            else
            {
                lamp.materials[1].SetColor("_EmissionColor", Color.black);
            }
            lastTime = Time.time;
        }
    }
}
