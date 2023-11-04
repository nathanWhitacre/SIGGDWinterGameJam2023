using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private float rayLength;
    [SerializeField] private float jumpStrength;
    private Vector3 movementVec;
    private bool grounded;
    public bool leftWall;
    [SerializeField] private AudioSource jumpSound;
    private int floorLayer;
    private LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        leftWall = true;
        floorLayer = LayerMask.NameToLayer("floor");
        mask = LayerMask.GetMask("albanian");
    }

    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == floorLayer)) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("albanian grounded");
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == floorLayer)) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        int groundMask = 1 << floorLayer;

        Vector3 leftPos = trans.position;
        leftPos.x -= 0.49f;
        Vector3 rightPos = trans.position;
        rightPos.x += 0.49f;

        if ((Physics.Raycast(leftPos, (trans.up * -1), rayLength, groundMask)) || (Physics.Raycast(rightPos, (trans.up * -1), rayLength, groundMask))) {
            grounded = true;
        }
        else {
            Debug.Log("uh oh retard alert");
            grounded = false;
        }



        float speed = moveSpeed;

        

        //horizontal movement
        if (Input.GetKey(KeyCode.A) == Input.GetKey(KeyCode.D)) {
            movementVec.x = 0;
        }
        else if (Input.GetKey(KeyCode.A)) {
            movementVec.x = -1;
            leftWall = true;
        }
        else {
            movementVec.x = 1;
            leftWall = false;
        }

        //jumping
        if (Input.GetKey(KeyCode.W) && grounded == true) {
            Debug.Log("albanian jump");
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = 5 * jumpStrength;
            body.velocity = vel;
            //jumpSound.Play();
        }

        Vector3 tempV = body.velocity;
        tempV.x = 0;
        tempV.z = 0;
        body.velocity = tempV;


        body.MovePosition(body.position + (movementVec * speed * Time.deltaTime));
    }
}
