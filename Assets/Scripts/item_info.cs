using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_info : MonoBehaviour
{   
    [SerializeField] private int itemID = 0;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private Sprite beerSprite;
    [SerializeField] private Sprite chickenSprite;
    [SerializeField] private Sprite shotgunSprite;
    [SerializeField] private Sprite ballSprite;
    [SerializeField] private GameObject childObject;
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
        Sprite niceSprite;
        if (itemID == 0) {
            niceSprite = beerSprite;
        }
        else if (itemID == 1) {
            niceSprite = chickenSprite;
        }
        else if (itemID == 2) {
            niceSprite = shotgunSprite;
        }
        else {
            niceSprite = ballSprite;
        }
        childObject.GetComponent<SpriteRenderer>().sprite = niceSprite;
    }
    public int getID() {
        return itemID;
    }
}
