using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rightPunchHitBox;
    [SerializeField] private GameObject leftPunchHitBox;


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (player.GetComponent<move_1>().facingLeft)
            {

                leftPunchHitBox.GetComponent<hitbox>().damageHitTargets(35);
            } else
            {
                rightPunchHitBox.GetComponent<hitbox>().damageHitTargets(35);
            }
        }


    }


}
