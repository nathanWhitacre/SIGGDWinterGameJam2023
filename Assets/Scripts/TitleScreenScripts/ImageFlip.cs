using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFlip : MonoBehaviour
{
    private float startTime = 0.0f;
    private float flipTime = 2.5f;
    private bool flipped = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - startTime > flipTime)
        {
            if (flipped)
            {
                GetComponent<Renderer>().enabled = true;
            } else
            {
                GetComponent<Renderer>().enabled = false;
            }
            startTime = Time.time;
            flipped = !flipped;
        }

    }
}
