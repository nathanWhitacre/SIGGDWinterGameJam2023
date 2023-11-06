using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_1 : MonoBehaviour
{   
    [SerializeField] private bool playerOne;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform trans;
    [SerializeField] private BoxCollider hurtbox;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator spriteAnimator;
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

    //private Vector3 downOffsetVec;

    private bool leftPushed;
    private bool rightPushed;
    private float leftTime;
    private float rightTime;
    private float dashTime;
    private bool trampd;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private float maxTramp;

    KeyCode leftKey;
    KeyCode rightKey;
    KeyCode upKey;
    KeyCode downKey;

    public float getSpeed() {
        return moveSpeed;
    }

    public void setSpeed(float sp) {
        moveSpeed = sp;
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerOne) {
            leftKey = KeyCode.A;
            rightKey = KeyCode.D;
            downKey = KeyCode.S;
            upKey = KeyCode.W;
        }
        else {
            leftKey = KeyCode.J;
            rightKey = KeyCode.L;
            downKey = KeyCode.K;
            upKey = KeyCode.I;
        }

        grounded = true;
        facingLeft = true;
        floorLayer = LayerMask.NameToLayer("floor");
        albanianLayer = LayerMask.NameToLayer("albanian");
        platformLayer = LayerMask.NameToLayer("platform");
        int groundMask_temp = 1 << floorLayer;
        int albanianMask_temp = 1 << albanianLayer;
        int platformMask_temp = 1 << platformLayer;
        groundMask = (groundMask_temp | albanianMask_temp) | platformMask_temp;
        
        //sideOffset = (trans.localScale.x / 2) - 0.01f;
        //rayLength = (trans.localScale.y / 2) + 0.01f;
        sideOffset = (hurtbox.size.x / 2) - 0.01f;
        rayLength = (hurtbox.size.y / 2) + 0.01f;
        //downOffsetVec = Vector3.zero;
        //downOffsetVec.y -= rayLength;
        //downOffsetVec = downOffsetVec * 0.5f;


        platCollide = trans.GetChild(0).gameObject.GetComponent<BoxCollider>();
       // platCollide = hurtbox.size.GetChild(0).gameObject.GetComponent<BoxCollider>();
        Debug.Log(platCollide);
        platCollide.enabled = false;

        leftPushed = false;
        rightPushed = false;
        inDash = false;
        trampd = false;
        
    }

    // Update is called once per frame
    void Update()
    {   

        
        Vector3 tempV = body.velocity;
        if ((tempV.y * -1) > terminalVelocity) {
            tempV.y = -1 * terminalVelocity;
        }

        spriteAnimator.SetFloat("verticalVelocity", tempV.y);
        spriteAnimator.SetBool("diving", inDash);
        if (inDash)
        {
            Debug.Log("Dashing BLEEEH");
        }
        if (spriteAnimator.GetBool("diving"))
        {
            Debug.Log("DIVING =================================");
        }

        Vector3 leftPos = trans.position;
        leftPos.x -= sideOffset;
        Vector3 rightPos = trans.position;
        rightPos.x += sideOffset;

        /*if ((Physics.Raycast((leftPos /*+ downOffsetVec), (trans.up * -1), out hit, ((rayLength * 0.5f) + 0.03f), groundMask)) || (Physics.Raycast((rightPos /*+ downOffsetVe), (trans.up * -1), out hit, ((rayLength * 0.5f) + 0.03f), groundMask)) || (Physics.Raycast((trans.position /*+ downOffsetVec), (trans.up * -1), out hit, ((rayLength * 0.5f) + 0.03f), groundMask))) {*/
        if ((Physics.Raycast((leftPos), (trans.up * -1), out hit, rayLength, groundMask)) || (Physics.Raycast((rightPos), (trans.up * -1), out hit, rayLength, groundMask)) || (Physics.Raycast((trans.position), (trans.up * -1), out hit, rayLength, groundMask)))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.layer == platformLayer) {
                Debug.Log("plat collide enabled");
                platCollide.enabled = true;
                if ((hitObject.tag == "trampoline") && (trampd == false)) {
                    tempV.y = tempV.y * -1;
                    tempV.y += 6;
                    if (tempV.y > (maxTramp * 0.7f)) {
                        tempV.y = maxTramp * 0.7f;
                    }
                    if (Input.GetKey(upKey)) {
                        tempV.y += 20;
                    }
                    if (tempV.y > maxTramp) {
                        tempV.y = maxTramp;
                    }
                    trampd = true;
                    grounded = false;
                    Debug.Log("trampd");
                }
                if (hitObject.tag != "trampoline") {
                    tempV.y = 0;
                    trampd = false;
                }
            }
            if ((hitObject.layer == floorLayer) || (hitObject.layer == albanianLayer)) {
                tempV.y = 0;
            }
            grounded = true;
        }
        else {
            Debug.Log("plat collide disabled");
            platCollide.enabled = false;
            grounded = false;
            trampd = false;
        }



        float speed = moveSpeed;
        
        //horizontal movement
        if (Input.GetKey(leftKey) == Input.GetKey(rightKey)) {
            spriteAnimator.SetBool("running", false);
            tempV.x -= (tempV.x / 2) * Time.deltaTime * decelSpeed;
        }
        else if (Input.GetKey(leftKey)) {
            tempV.x += (((-1 * moveSpeed) - tempV.x) / 2) * Time.deltaTime * accelSpeed;
            facingLeft = true;
            spriteRenderer.flipX = true;
            spriteAnimator.SetBool("running", true);
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
            spriteRenderer.flipX = false;
            spriteAnimator.SetBool("running", true);
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


        if (Input.GetKey(leftKey) == false) {
            leftPushed = false;
        }
        if (Input.GetKey(rightKey) == false) {
            rightPushed = false;
        }

        //jumping
        if (Input.GetKey(upKey) && (grounded == true) && (tempV.y <= 0)) {
            Debug.Log("albanian jump");
            grounded = false;
            tempV.y += 5 * jumpStrength;
            //jumpSound.Play();
        }

        if (inDash) {
            tempV.y += gravResist * Time.deltaTime;
            if (Time.time - dashTime > dashTimeLength) {
                inDash = false;
            }
        }

        tempV.z = 0;
        body.velocity = tempV;

        
    }
}
