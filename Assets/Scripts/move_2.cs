using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private float rayLength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float decelSpeed;
    private bool grounded;
    public bool facingLeft;
    [SerializeField] private AudioSource jumpSound;
    private int floorLayer;
    private int albanianLayer;
    private int groundMask;


    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        facingLeft = true;
        floorLayer = LayerMask.NameToLayer("floor");
        albanianLayer = LayerMask.NameToLayer("albanian");
        int groundMask_temp = 1 << floorLayer;
        int albanianMask_temp = 1 << albanianLayer;
        groundMask = groundMask_temp | albanianMask_temp;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 leftPos = trans.position;
        leftPos.x -= 0.49f;
        Vector3 rightPos = trans.position;
        rightPos.x += 0.49f;

        if ((Physics.Raycast(leftPos, (trans.up * -1), rayLength, groundMask)) || (Physics.Raycast(rightPos, (trans.up * -1), rayLength, groundMask))) {
            grounded = true;
        }
        else {
            grounded = false;
        }

        Vector3 tempV = body.velocity;

        float speed = moveSpeed;
        
        //horizontal movement
        if (Input.GetKey(KeyCode.J) == Input.GetKey(KeyCode.L)) {
            tempV.x -= (tempV.x / 2) * Time.deltaTime * decelSpeed;
        }
        else if (Input.GetKey(KeyCode.J)) {
            tempV.x += (((-1 * moveSpeed) - tempV.x) / 2) * Time.deltaTime * accelSpeed;
            facingLeft = true;
        }
        else {
            tempV.x += ((moveSpeed - tempV.x) / 2) * Time.deltaTime * accelSpeed;
            facingLeft = false;
        }


        //jumping
        if (Input.GetKey(KeyCode.I) && grounded == true) {
            Debug.Log("albanian jump");
            grounded = false;
            tempV.y = 5 * jumpStrength;
            //jumpSound.Play();
        }


        tempV.z = 0;
        body.velocity = tempV;


        //body.MovePosition(body.position + (movementVec * speed * Time.deltaTime));
    }
}
