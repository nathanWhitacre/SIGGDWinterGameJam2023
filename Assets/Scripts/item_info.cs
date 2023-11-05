using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_info : MonoBehaviour
{   
    public int itemID;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float terminalVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempV = body.velocity;
        tempV.y = (terminalVelocity * -1);
        body.velocity = tempV;
    }
}
