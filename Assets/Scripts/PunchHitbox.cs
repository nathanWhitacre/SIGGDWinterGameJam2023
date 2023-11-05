using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PunchHitbox : MonoBehaviour
{

    private List<Transform> hitTargets;


    private void Start()
    {
        hitTargets = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered");
        hitTargets.Add(other.gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        hitTargets.Remove(other.gameObject.transform);
    }


    public void damageHitTargets(float damage, float knockback, Vector3 direction)
    {
        foreach (Transform target in hitTargets)
        {
            health targetHealth = target.GetComponent<health>();
            if (targetHealth != null)
            {
                targetHealth.damage(damage);
                target.gameObject.GetComponent<Rigidbody>().AddForce((knockback * direction), ForceMode.Impulse);
                Debug.Log("Damaged " + target.name + " for " + damage + " damage");
            }
        }
    }
}
