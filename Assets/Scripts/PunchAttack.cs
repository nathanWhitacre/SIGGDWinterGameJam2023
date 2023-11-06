using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] private int baseDamage = 0;
    private int damage;
    [SerializeField] private int drunkDmg = 4;
    [SerializeField] private float baseLungeImpulse = 5;
    private float lungeImpulse;
    [SerializeField] private float drunkLunge = 3;
    [SerializeField] private float baseKnockback = 5;
    private float knockback;
    [SerializeField] private float drunkKnockback = 3;
    [SerializeField] private KeyCode inputButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rightPunchHitBox;
    [SerializeField] private GameObject leftPunchHitBox;
    [SerializeField] private Animator spriteAnimator;

    private bool isPunching = false;
    private float punchLingerTime = 0.4f;
    private float punchStartTime;
    private int previousPunch = -1;


    void Start() {
        damage = baseDamage;
        knockback = baseKnockback;
        lungeImpulse = baseLungeImpulse;
    }

    // Update is called once per frame
    void Update()
    {

        spriteAnimator.SetBool("punching", isPunching);
        if (isPunching)
        {
            Debug.Log("FALCON PUNCH");
        }
        if (spriteAnimator.GetBool("punching"))
        {
            Debug.Log("ROCKET PUNCH ==================================");
        }

        if (Input.GetKeyDown(inputButton))
        {

            int randomPunch = Mathf.FloorToInt(Random.Range(0f, 3.999f));
            if (randomPunch == previousPunch)
            {
                randomPunch = (previousPunch - 1 >= 0) ? previousPunch - 1 : 3;
            }

            spriteAnimator.SetInteger("randomPunch", randomPunch);
            isPunching = true;
            punchStartTime = Time.time;

            previousPunch = randomPunch;

            Rigidbody playerRigidBody = player.gameObject.GetComponent<Rigidbody>();
            if (player.GetComponent<move_1>().facingLeft)
            {
                playerRigidBody.AddForce((lungeImpulse * Vector3.left), ForceMode.Impulse);
                leftPunchHitBox.GetComponent<PunchHitbox>().damageHitTargets(damage, knockback, Vector3.left);
            } else
            {
                playerRigidBody.AddForce((lungeImpulse * Vector3.right), ForceMode.Impulse);
                rightPunchHitBox.GetComponent<PunchHitbox>().damageHitTargets(damage, knockback, Vector3.right);
            }
        }


        if (isPunching && Time.time - punchStartTime > punchLingerTime)
        {
            isPunching = false;
        }


    }

    public void setDrunkDmg(int beerLevel) {
        damage = baseDamage + (beerLevel * drunkDmg);
        knockback = baseKnockback + (beerLevel * drunkKnockback);
        lungeImpulse = baseLungeImpulse + (beerLevel * drunkLunge);
    }



}
