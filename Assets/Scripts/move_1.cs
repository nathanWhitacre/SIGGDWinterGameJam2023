using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    private float rayLength;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float decelSpeed;
    private bool grounded;
    public bool facingLeft;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private float dashDelay;
    [SerializeField] private float dashStrength;
    [SerializeField] private float dashTimeLength;
    [SerializeField] private float gravResist;
    private bool inDash;
    private int floorLayer;
    private int albanianLayer;
    private int groundMask;
    private float sideOffset;
    private int platformLayer;
    private RaycastHit hit;
    private BoxCollider platCollide;

    private bool leftPushed;
    private bool rightPushed;
    private float leftTime;
    private float rightTime;
    private float dashTime;


    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        facingLeft = true;
        floorLayer = LayerMask.NameToLayer("floor");
        albanianLayer = LayerMask.NameToLayer("albanian");
        platformLayer = LayerMask.NameToLayer("platform");
        int groundMask_temp = 1 << floorLayer;
        int albanianMask_temp = 1 << albanianLayer;
        int platformMask_temp = 1 << platformLayer;
        groundMask = (groundMask_temp | albanianMask_temp) | platformMask_temp;
        
        sideOffset = (trans.localScale.x / 2) - 0.01f;
        rayLength = (trans.localScale.y / 2) + 0.01f;

        platCollide = trans.GetChild(0).gameObject.GetComponent<BoxCollider>();
        Debug.Log(platCollide);
        platCollide.enabled = false;

        leftPushed = false;
        rightPushed = false;
        inDash = false;
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        Vector3 leftPos = trans.position;
        leftPos.x -= sideOffset;
        Vector3 rightPos = trans.position;
        rightPos.x += sideOffset;

        if ((Physics.Raycast(leftPos, (trans.up * -1), out hit, rayLength, groundMask)) || (Physics.Raycast(rightPos, (trans.up * -1), out hit, rayLength, groundMask)) || (Physics.Raycast(trans.position, (trans.up * -1), out hit, rayLength, groundMask))) {
            if (hit.transform.gameObject.layer == platformLayer) {
                Debug.Log("plat collide enabled");
                platCollide.enabled = true;
            }
            grounded = true;
        }
        else {
            Debug.Log("plat collide disabled");
            platCollide.enabled = false;
            grounded = false;
        }

        Vector3 tempV = body.velocity;

        float speed = moveSpeed;
        
        //horizontal movement
        if (Input.GetKey(KeyCode.A) == Input.GetKey(KeyCode.D)) {
            tempV.x -= (tempV.x / 2) * Time.deltaTime * decelSpeed;
        }
        else if (Input.GetKey(KeyCode.A)) {
            tempV.x += (((-1 * moveSpeed) - tempV.x) / 2) * Time.deltaTime * accelSpeed;
            facingLeft = true;
            if (leftPushed == false) {
                if (((Time.time - leftTime) < dashDelay) && (inDash == false)) {
                    tempV.x += (-1) * dashStrength * 5;
                    tempV.y = 0;
                    inDash = true;
                    dashTime = Time.time;
                }
                leftPushed = true;
                leftTime = Time.time;
            }
        }
        else {
            tempV.x += ((moveSpeed - tempV.x) / 2) * Time.deltaTime * accelSpeed;
            facingLeft = false;
            if (rightPushed == false) {
                if (((Time.time - rightTime) < dashDelay) && (inDash == false)) {
                    tempV.x += dashStrength * 5;
                    tempV.y = 0;
                    inDash = true;
                    dashTime = Time.time;
                }
                rightPushed = true;
                rightTime = Time.time;
            }
        }

        if (inDash) {
            tempV.y += gravResist * Time.deltaTime;
            if (Time.time - dashTime > dashTimeLength) {
                inDash = false;
            }
        }

        if (Input.GetKey(KeyCode.A) == false) {
            leftPushed = false;
        }
        if (Input.GetKey(KeyCode.D) == false) {
            rightPushed = false;
        }



        //jumping
        if (Input.GetKey(KeyCode.W) && grounded == true) {
            Debug.Log("albanian jump");
            grounded = false;
            tempV.y = 5 * jumpStrength;
            //jumpSound.Play();
        }


        tempV.z = 0;
        body.velocity = tempV;

        
    }
}
