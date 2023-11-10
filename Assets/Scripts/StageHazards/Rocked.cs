using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocked : MonoBehaviour
{
    public float force;
    public float radius;
    public float offset;
    public float damage;
    public int type = 3;
    public float duration = 2f;
    private float instTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 22)
        {
            collision.rigidbody.AddExplosionForce(force, this.transform.position - new Vector3(0, -offset, 0), radius);
            collision.gameObject.GetComponent<health>().damage(damage, type);
            Debug.Log("EXPLODE!");
        }
        Destroy(gameObject);
    }

    void Start() {
        instTime = Time.time;
    }

    void Update() {
        if (Time.time - instTime > duration) {
            Destroy(gameObject);
        }
    }
}
