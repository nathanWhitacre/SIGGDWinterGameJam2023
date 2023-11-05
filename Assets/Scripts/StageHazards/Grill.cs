using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    [SerializeField] private float burn_delay;
    [SerializeField] private float burn_dmg;
    int playerLayer = 22;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            Burning burn = collider.gameObject.AddComponent<Burning>();
            burn.SetDamage(burn_dmg);
            burn.SetDelay(burn_delay);
            Debug.Log("Begin the Burn!");
        }        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            Destroy(collider.gameObject.GetComponent<Burning>());
            Debug.Log("Burn Off");
        }
    }
}
