using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloatingAla : MonoBehaviour
{
    private float speed = 1f;
    private float amount = 1f;
    private float speed2 = 1.5f;
    private float amount2 = 1f;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bobb = startPosition;
        bobb.x += Mathf.Sin(Time.time * speed) * amount;
        bobb.y += Mathf.Sin(Time.time * speed2) * amount2;
        transform.position = bobb;
    }
}
