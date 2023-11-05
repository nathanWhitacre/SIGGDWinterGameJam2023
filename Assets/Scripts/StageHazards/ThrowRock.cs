using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float width;
    [SerializeField] private GameObject rock;
    [SerializeField] private float delay;
    [SerializeField] private float force;

    private float last_rock = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 direction = Vector3.down;
        Vector3 lbound = transform.position - new Vector3(width/2, 0, 0);
        Vector3 rbound = transform.position + new Vector3(width/2, 0, 0);
        //Vector3 endpoint = transform.position - new Vector3(0, 64, 0);
        if (Physics.CapsuleCast(lbound, rbound, 1, direction, out hit, 64, mask))
        {
            float current = Time.time;
            float difference =  current - last_rock;

            if (difference >= delay)
            {
                Hit();
                last_rock = current;
            }
            //Debug.Log("Did Hit");
        }

        Debug.DrawLine(lbound, lbound - new Vector3(0, 64, 0), Color.white);
        Debug.DrawLine(rbound, rbound - new Vector3(0, 64, 0), Color.white);
    }

    void Hit()
    {
        GameObject proj = Instantiate(rock, this.transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody>().AddForce(new Vector3(0, -force, 0), ForceMode.Impulse);
    }
}
