using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    [SerializeField] private float burn_delay;
    [SerializeField] private float burn_dmg;
    [SerializeField] private float burn_duration;
    int playerLayer = 22;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            // Reset burn if it exists
            Burning burn = collider.gameObject.GetComponent<Burning>();

            if (burn == null)
            {
                burn = collider.gameObject.AddComponent<Burning>();
                burn.SetDamage(burn_dmg);
                burn.SetDelay(burn_delay);
                burn.SetDuration(burn_duration);
            } else
            {
                burn.BurnReset();
            }

            Debug.Log("Begin the Burn!");
        }        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == playerLayer)
        {
            collider.gameObject.GetComponent<Burning>().SelfDestruct();
        }
    }
}
