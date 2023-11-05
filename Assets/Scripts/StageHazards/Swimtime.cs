using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimtime : MonoBehaviour
{
    [SerializeField] float slowFactor;
    int playerLayer = 22;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            float speed = collider.gameObject.GetComponent<move_1>().getSpeed();
            collider.gameObject.GetComponent<move_1>().setSpeed(speed * slowFactor);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            float speed = collider.gameObject.GetComponent<move_1>().getSpeed();
            collider.gameObject.GetComponent<move_1>().setSpeed(speed / slowFactor);
        }
    }
}
