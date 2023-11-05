using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimtime : MonoBehaviour
{
    int playerLayer = 22;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {

        }
    }
}
