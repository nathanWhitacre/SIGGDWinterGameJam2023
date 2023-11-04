using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class hitbox : MonoBehaviour
{

    private List<Transform> hitTargets;


    private void Start()
    {
        hitTargets = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        hitTargets.Add(other.gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        hitTargets.Remove(other.gameObject.transform);
    }


    public void damageHitTargets(float damage)
    {
        foreach (Transform target in hitTargets)
        {
            health targetHealth = target.GetComponent<health>();
            if (targetHealth != null)
            {
                targetHealth.damage(damage);
                Debug.Log("Damaged " + target.name + " for " + damage + " damage");
            }
        }
    }
}
