using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocked : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float radius;
    [SerializeField] private float offset;
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 22)
        {
            collision.rigidbody.AddExplosionForce(force, this.transform.position - new Vector3(0, -offset, 0), radius);
            collision.gameObject.GetComponent<health>().damage(damage);
            Debug.Log("EXPLODE!");
        }
        Destroy(gameObject);
    }
}
