using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_info : MonoBehaviour
{   
    private int itemID;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        itemID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempV = body.velocity;
        tempV.y = (terminalVelocity * -1);
        body.velocity = tempV;
    }

    public void setID(int newID) {
        itemID = newID;
    }
    public int getID() {
        return itemID;
    }
}
