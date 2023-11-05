using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{

    [SerializeField] private int damage = 0;
    [SerializeField] private float lungeImpulse = 5;
    [SerializeField] private float knockback = 5;
    [SerializeField] private KeyCode inputButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rightPunchHitBox;
    [SerializeField] private GameObject leftPunchHitBox;


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


}
