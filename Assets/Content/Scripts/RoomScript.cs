using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> exits = new List<GameObject>();
    private float[] rotateExits;


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


    void Awake()
    {
        rotateExits = new float[exits.Count];
        for (int i = 0; i < exits.Count; i++)
        {
            rotateExits[i] = 90 * Math.Sign(exits[i].transform.localPosition.z);
        }
    }
}
