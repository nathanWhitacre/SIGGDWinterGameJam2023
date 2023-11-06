using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public class Opposite : MonoBehaviour
{
    [SerializeField] private float chance;
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private float minoppRate;
    [SerializeField] private float oppDuration;
    [SerializeField] private PostProcessVolume vol;


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
        this.gameObject.GetComponent<Match>().playOppositeDay();
        last_opposite = Time.time;
        Debug.Log("Opposite Time!");
        vol.weight = 1f;
    }

    public void NormalTime()
    {
        isOppositeDay = false;
        next_opposite = Time.time + minoppRate + Random.RandomRange(0f, 25f);
        Debug.Log("Normal Time!");
        vol.weight = 0f;
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
