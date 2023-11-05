using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opposite : MonoBehaviour
{
    [SerializeField] private float chance;
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private float oppRate;
    [SerializeField] private float oppDuration;
    [SerializeField] private GameObject[] reveal;


    public bool isOppositeDay = false;
    private float next_opposite;
    private float last_opposite;

    // Start is called before the first frame update
    void Start()
    {
        NormalTime();
    }

    // Update is called once per frame
    void Update()
    {
        float current = Time.time;
        if (!isOppositeDay)
        {

            if (current >= next_opposite)
            {
                OppositeTime();
            }
        } else
        {
            if (current >= oppDuration + last_opposite)
            {
                NormalTime();
            } 
        }
        
        
    }

    void OppositeTime()
    {
        isOppositeDay = true;
        last_opposite = Time.time;
        Debug.Log("Opposite Time!");
        foreach(GameObject go in reveal)
        {
            go.active = true;
        }
    }

    void NormalTime()
    {
        isOppositeDay = false;
        next_opposite = Time.time + oppRate;
        Debug.Log("Normal Time!");
        foreach (GameObject go in reveal)
        {
            go.active = false;
        }
    }


    // Getters & Setters

    public GameObject getP1()
    {
        return P1;
    }

    public GameObject getP2()
    {
        return P2;
    }
   
}
