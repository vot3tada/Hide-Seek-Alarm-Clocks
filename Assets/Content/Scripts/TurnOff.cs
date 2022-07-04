using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    [SerializeField] private Light light;
    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        light.enabled = false;
        lastTime = Time.time;
    }

    // Update is called once per frame
    public void SwitchLight()
    {
        if (Time.time - lastTime > 0.4f)
        {
            light.enabled = !light.enabled;
            lastTime = Time.time;
        }
    }
}
