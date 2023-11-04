using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_2 : MonoBehaviour
{

    [SerializeField] private int damage = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rightPunchHitBox;
    [SerializeField] private GameObject leftPunchHitBox;


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Period))
        {
            Rigidbody playerRigidBody = player.gameObject.GetComponent<Rigidbody>();
            if (player.GetComponent<move_2>().facingLeft)
            {
                playerRigidBody.AddForce((5 * Vector3.left), ForceMode.Impulse);
                leftPunchHitBox.GetComponent<hitbox>().damageHitTargets(damage, Vector3.left);
            } else
            {
                playerRigidBody.AddForce((5 * Vector3.right), ForceMode.Impulse);
                rightPunchHitBox.GetComponent<hitbox>().damageHitTargets(damage, Vector3.right);
            }
        }


    }


}
