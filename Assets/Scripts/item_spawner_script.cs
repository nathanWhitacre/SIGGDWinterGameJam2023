using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class item_spawner_script : MonoBehaviour
{   
    [SerializeField] private Transform trans;
    [SerializeField] private float spread;
    [SerializeField] private float freq;
    [SerializeField] private GameObject itemBox;
    private Vector3 spanVec;
    private float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        spanVec = Vector3.zero;
        spanVec.x = spread;
        prevTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spanVec.x = spread;
        Debug.DrawLine(trans.position, (trans.position + spanVec), Color.red);
        Debug.DrawLine(trans.position, (trans.position - spanVec), Color.red);
        if (Time.time - prevTime > freq) {
            prevTime = Time.time;
            this.SpawnItem();
        }
    }

    void SpawnItem() {
        float pos = Random.Range(-1f, 1f) * spread;
        Vector3 tempSpawn = trans.position;
        tempSpawn.x += pos;
        GameObject item = Instantiate(itemBox, tempSpawn, Quaternion.identity);
        int randVal = Random.Range(1, 11);
        Debug.Log(randVal);
        if (randVal < 4) {
            item.GetComponent<item_info>().setID(0);  //beer
        }
        else if (randVal < 7) {
            item.GetComponent<item_info>().setID(1);  //chicken
        }
        else if (randVal < 9) {
            item.GetComponent<item_info>().setID(3);  //bowling ball
        }
        else {
            item.GetComponent<item_info>().setID(2);  //shotgun
        }
    }
}
