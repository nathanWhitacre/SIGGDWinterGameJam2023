using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    private float last_burn;
    private float delay;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        last_burn = Time.time;
        
    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "Fireplace")
        {
            float current = Time.time;
            float difference = current - last_burn;

            if (difference >= delay)
            {
                this.GetComponent<health>().damage(damage);
                Debug.Log("BURN BABY BURN!");
            }

        }
    }

    public void SetDelay(float delay)
    {
        this.delay = delay;
    }

    public void SetDamage(float damange)
    {
        this.damage = damange;
    }
}
