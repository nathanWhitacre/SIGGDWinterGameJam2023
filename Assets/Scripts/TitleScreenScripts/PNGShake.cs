using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNGShake : MonoBehaviour
{
    [SerializeField] private float offset;
    private float speed = 100f;
    private float amount = 0.05f;
    private float speed2 = 50f;
    private float amount2 = 0.05f;
    private GameObject Person;
    Vector3 startPosition; 
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bob = startPosition;
        bob.x += Mathf.Sin(Time.time * speed * offset) * amount;
        bob.y += Mathf.Sin(Time.time * speed2 * offset) * amount2;
        transform.position = bob;
    }
}
