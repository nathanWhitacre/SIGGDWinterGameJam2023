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
        //item type set here
        item.GetComponent<item_info>().itemID = 0;
    }
}
