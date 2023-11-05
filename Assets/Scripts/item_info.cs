using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_info : MonoBehaviour
{   
    [SerializeField] private int itemID = 0;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float terminalVelocity;
    // Start is called before the first frame update

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
