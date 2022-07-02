using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCyrcleManager: MonoBehaviour
{
    [Range(0, 1), SerializeField] private float timeOfDay;
    [SerializeField] private float dayDuration = 60f;
    [SerializeField] private Light Sun;
    [SerializeField] private AnimationCurve SunCurve;
    private float sunIntensity;

    public float DayDuration
    {
        set 
        {
            if (value < 60 || value > 600)
                throw new System.Exception("MADE IN HEAVEN!!!");
            dayDuration = value*2;
        }
    }

    private void Start()
    {
        sunIntensity = Sun.intensity;
    }


    void FixedUpdate()
    {
        timeOfDay += (Time.fixedDeltaTime / dayDuration) % 1;

        Sun.transform.localRotation = Quaternion.Euler(timeOfDay* 360f, 180f, 0);

        Sun.intensity = sunIntensity * SunCurve.Evaluate(timeOfDay);
    }
}
