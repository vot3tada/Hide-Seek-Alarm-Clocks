using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> exits = new List<GameObject>();
    [SerializeField] private List<GameObject> plugs = new List<GameObject>();
    [SerializeField] private GameObject center;
    private float[] rotateExits;
    private float[] rotatePlugs;

    public Transform Center => center.transform;

    public (GameObject, float)[] Exits
    {
        get
        {
            (GameObject, float)[] exits = new (GameObject, float)[this.exits.Count];
            for (int i = 0; i < exits.Length; i++)
            {
                exits[i] = (this.exits[i], rotateExits[i]);
            }
            return exits;
        }
    }

    public (GameObject, float)[] Plugs
    {
        get
        {
            (GameObject, float)[] plugs = new (GameObject, float)[this.plugs.Count];
            for (int i = 0; i < plugs.Length; i++)
            {
                plugs[i] = (this.plugs[i], rotatePlugs[i]);
            }
            return plugs;
        }
    }


    void Awake()
    {
        rotateExits = new float[exits.Count];
        for (int i = 0; i < exits.Count; i++)
        {
            rotateExits[i] = 90 * Math.Sign(exits[i].transform.localPosition.z);
        }

        rotatePlugs = new float[plugs.Count];
        for (int i = 0; i < plugs.Count; i++)
        {
            rotatePlugs[i] = 90 * Math.Sign(plugs[i].transform.localPosition.z);
        }
    }
}
