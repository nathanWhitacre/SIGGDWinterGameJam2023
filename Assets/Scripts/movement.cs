using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        leftWall = true;
    }

    private LayerMask mask = LayerMask.GetMask("alabamian");
    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)) {
                Debug.Log("alabamian grounded");
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        if ((col.gameObject.layer == LayerMask.NameToLayer("platforms"))) {
            if (Physics.Raycast(trans.position, (trans.up * -1), rayLength, ~mask)
            && !(Physics.Raycast(trans.position, (trans.right), rayLength, ~mask) || Physics.Raycast(trans.position, (trans.right * -1), rayLength, ~mask))) {
                grounded = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        int groundMask = 1 << LayerMask.NameToLayer("platforms");

        Vector3 leftPos = trans.position;
        leftPos.x -= 0.74f;
        Vector3 rightPos = trans.position;
        rightPos.x += 0.74f;

        if ((Physics.Raycast(leftPos, (trans.up * -1), rayLength, groundMask)) || (Physics.Raycast(rightPos, (trans.up * -1), rayLength, groundMask))) {
            grounded = true;
        }
        else {
            Debug.Log("uh oh retard alert");
            grounded = false;
        }



        float speed = moveSpeed;

        Vector3 tempV = body.velocity;
        tempV.x = 0;
        tempV.z = 0;
        body.velocity = tempV;

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
            Debug.Log("dog jump");
            grounded = false;
            Vector3 vel = body.velocity;
            vel.y = 5 * jumpStrength;
            body.velocity = vel;
            //jumpSound.Play();
        }


        body.MovePosition(body.position + (movementVec * speed * Time.deltaTime));
    }
}
