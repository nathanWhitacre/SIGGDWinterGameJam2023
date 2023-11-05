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


    void Start() {
        damage = baseDamage;
        knockback = baseKnockback;
        lungeImpulse = baseLungeImpulse;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(inputButton))
        {
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


    }

    public void setDrunkDmg(int beerLevel) {
        damage = baseDamage + (beerLevel * drunkDmg);
        knockback = baseKnockback + (beerLevel * drunkKnockback);
        lungeImpulse = baseLungeImpulse + (beerLevel * drunkLunge);
    }


}
